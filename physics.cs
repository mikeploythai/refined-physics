// Main purpose of this document is to get C# to show up in the languages section lol

Code "Sonic - Universal Changes" by "cali_burrito"
//
  #include "Reflection" noemit

  #lib "Reflection"
  #lib "SonicParameters"
  #lib "Player"
  #lib "INI"

  using System.IO;

  static bool isLoaded = false;
  static string directory;
  static float chargeCount = 0f;
//
{
  if (!isLoaded) {
    directory = Path.Combine(Directory.GetCurrentDirectory(), "Mods", "Refined Physics");
    isLoaded = true;
  }

  var config = INI.Read(Path.Combine(directory, "config.ini"));
  bool isMomentumEnabled = bool.Parse(config["global"]["momentum"]);
  bool isRailMomentumEnabled = bool.Parse(config["global"]["railMomentum"]);
  bool isChargableSpinDashEnabled = bool.Parse(config["global"]["chargableSpinDash"]);
  bool isNoSpinChargeDeceleEnabled = bool.Parse(config["global"]["noSpinChargeDecele"]);
  bool isModifiedDashGravityEnabled = bool.Parse(config["global"]["modifiedDashGravity"]);
  bool isBalancedBoostEnabled = bool.Parse(config["global"]["balancedBoost"]);
  bool isDisableSpinMoveEnabled = bool.Parse(config["global"]["disableSpinMove"]);

  if (isDisableSpinMoveEnabled)
    Player.State.Redirect<Sonic.StateID>(Sonic.StateID.StateSpinMove, Sonic.StateID.StateStand);
  
  if (isNoSpinChargeDeceleEnabled) {
    var kinematics = Player.Kinematics.Get();
    if (kinematics == null) return;

    if (!Player.Status.IsGrounded() && Player.Input.IsDown(Player.InputActionType.PlayerSonicboom) && Player.State.GetCurrentStateID<Sonic.StateID>() == Sonic.StateID.StateSpinBoostCharge)
      kinematics->Velocity += Player.Kinematics.GetForward();
  }

  var SonicParams = Reflection.GetDataInfo<SonicParameters.Root>("player_common");
  if (SonicParams.pData == null) return;

  if (isMomentumEnabled) {
    RFL_SET_CONTEXTUAL_PLAYER_PARAM(SonicParams, modePackage.speed.decele.force, 4f);
    RFL_SET_CONTEXTUAL_PLAYER_PARAM(SonicParams, modePackage.speed.decele.force2, 4f);
    RFL_SET_CONTEXTUAL_PLAYER_PARAM(SonicParams, modePackage.jumpSpeed.limitUpSpeed, 30f);
    RFL_SET_CONTEXTUAL_PLAYER_PARAM(SonicParams, spinBoost.speedBoost.decele.force, 4f);
    RFL_SET_CONTEXTUAL_PLAYER_PARAM(SonicParams, spinBoost.speedBoost.decele.force2, 4f);
  }

  if (isRailMomentumEnabled) {
    RFL_SET_CONTEXTUAL_PLAYER_PARAM(SonicParams, modePackage.grind.acceleForce, 5f);
    RFL_SET_CONTEXTUAL_PLAYER_PARAM(SonicParams, modePackage.grind.deceleForce, 4f);
    RFL_SET_CONTEXTUAL_PLAYER_PARAM(SonicParams, modePackage.grind.limitSpeedMin, 8f);
  }

  if (isChargableSpinDashEnabled) {
    float dashVelocity = Player.Kinematics.GetHorizontalMagnitude();
    float minSpeed = Player.Status.IsSideView() ? 25f : 35f;
    float maxSpeed = Player.Status.IsSideView() ? 55f : 65f;

    if (Player.Status.IsGrounded() && Player.State.GetCurrentStateID<Sonic.StateID>() == Sonic.StateID.StateSpinBoostCharge) {
      if (Player.Input.IsPressed(Player.InputActionType.PlayerSonicboom)) chargeCount += 1f;
      dashVelocity = MathHelpers.Clamp((minSpeed - 7.5f) + (chargeCount * 7.5f), minSpeed, maxSpeed);
    } else if (!Player.Status.IsGrounded() && Player.State.GetCurrentStateID<Sonic.StateID>() == Sonic.StateID.StateSpinBoostCharge) {
      chargeCount += 1f / 1.125f;
      dashVelocity = MathHelpers.Clamp(chargeCount, minSpeed, maxSpeed);
    } else if (Player.State.GetPreviousStateID<Sonic.StateID>() != Sonic.StateID.StateSpinBoostCharge && Player.State.GetCurrentStateID<Sonic.StateID>() != Sonic.StateID.StateSpinBoost) {
      chargeCount = 0f;
      dashVelocity = Player.Kinematics.GetHorizontalMagnitude();
    }

    RFL_SET_CONTEXTUAL_PLAYER_PARAM(SonicParams, spinBoost.speedBoost.initialSpeed, dashVelocity);
    RFL_SET_CONTEXTUAL_PLAYER_PARAM(SonicParams, spinBoost.speedBoost.maxSpeed, isMomentumEnabled ? 3f : Player.Kinematics.GetHorizontalMagnitude());
  }

  if (isBalancedBoostEnabled)
    RFL_SET_CONTEXTUAL_PLAYER_PARAM(SonicParams, modePackage.boost.consumptionRate, Player.State.GetCurrentStateID<Sonic.StateID>() != Sonic.StateID.StateSpinBoost ? 100f : 20f);

  if (isModifiedDashGravityEnabled) {
    RFL_SET_CONTEXTUAL_PLAYER_PARAM(SonicParams, spinBoost.gravitySize, 70f);
    RFL_SET_CONTEXTUAL_PLAYER_PARAM(SonicParams, spinBoost.gravitySizeMinInAir, 40f);
  }
}

Code "Sonic - Open Zone Changes" by "cali_burrito"
//
  #include "Reflection" noemit

  #lib "Reflection"
  #lib "SonicParameters"
  #lib "INI"

  using System.IO;

  static bool isLoaded = false;
  static string directory;
//
{
  if (!isLoaded) {
    directory = Path.Combine(Directory.GetCurrentDirectory(), "Mods", "Refined Physics");
    isLoaded = true;
  }

  var config = INI.Read(Path.Combine(directory, "config.ini"));
  bool isFasterRunningEnabled = bool.Parse(config["openZone"]["fasterRunning"]);
  bool isCyberAirBoostEnabled = bool.Parse(config["openZone"]["cyberAirBoost"]);
  bool isFixedSlideEnabled = bool.Parse(config["openZone"]["fixedSlide"]);
  bool isWallRunEaseEnabled = bool.Parse(config["openZone"]["wallRunEase"]);
  bool isMomentumEnabled = bool.Parse(config["global"]["momentum"]);
  bool isBalancedBoostEnabled = bool.Parse(config["global"]["balancedBoost"]);

  var SonicParams = Reflection.GetDataInfo<SonicParameters.Root>("player_common");
  if (SonicParams.pData == null) return;

  if (isFasterRunningEnabled) {
    RFL_SET_PARAM(SonicParams, forwardView.modePackage.speed.normal.max, 20f);
    RFL_SET_PARAM(SonicParams, forwardView.modePackage.speed.normal2.max, 20f);
    RFL_SET_PARAM(SonicParams, forwardView.modePackage.speed.boost.initial, 40f);
    RFL_SET_PARAM(SonicParams, forwardView.modePackage.speed.boost.min, 30f);
    RFL_SET_PARAM(SonicParams, forwardView.modePackage.speed.boost.max, 40f);
    RFL_SET_PARAM(SonicParams, forwardView.modePackage.speed.boost2.initial, 40f);
    RFL_SET_PARAM(SonicParams, forwardView.modePackage.speed.boost2.min, 30f);
    RFL_SET_PARAM(SonicParams, forwardView.modePackage.speed.boost2.max, 40f);
  }

  if (isCyberAirBoostEnabled) {
    RFL_SET_PARAM(SonicParams, forwardView.modePackage.airboost.startHSpeed, 40f);
    RFL_SET_PARAM(SonicParams, forwardView.modePackage.airboost.startHSpeedMax, 80f);
    RFL_SET_PARAM(SonicParams, forwardView.modePackage.airboost.minHSpeed, 30f);
    RFL_SET_PARAM(SonicParams, forwardView.modePackage.airboost.minHSpeedMax, 60f);
    RFL_SET_PARAM(SonicParams, forwardView.modePackage.airboost.minKeepTime, 0.05f);
    RFL_SET_PARAM(SonicParams, forwardView.modePackage.airboost.maxKeepTime, 0.1f);
    RFL_SET_PARAM(SonicParams, forwardView.modePackage.airboost.maxTime, 0f);
    RFL_SET_PARAM(SonicParams, forwardView.modePackage.airboost.gravityRate, 0.5f);
    RFL_SET_PARAM(SonicParams, forwardView.modePackage.airboost.additionalTransitTime, 0.3f);
  }

  if (isFixedSlideEnabled)
    RFL_SET_PARAM(SonicParams, forwardView.modePackage.sliding.minSpeed, 8f);
  
  if (isWallRunEaseEnabled)
    RFL_SET_PARAM(SonicParams, forwardView.modePackage.wallmove.brake, 20f);
  
  if (isMomentumEnabled)
    RFL_SET_PARAM(SonicParams, forwardView.modePackage.fall.overSpeedDeceleForce, 0f);
  
  if (isBalancedBoostEnabled) {
    RFL_SET_PARAM(SonicParams, forwardView.modePackage.boost.recoveryRate, 20f);
    RFL_SET_PARAM(SonicParams, forwardView.modePackage.boost.infinityBoostTime, 120f);
  }
}

Code "Cyber Space Changes" by "cali_burrito"
//
  #include "Reflection" noemit

  #lib "Reflection"
  #lib "SonicParameters"
  #lib "INI"

  using System.IO;

  static bool isLoaded = false;
  static string directory;
//
{
  if (!isLoaded) {
    directory = Path.Combine(Directory.GetCurrentDirectory(), "Mods", "Refined Physics");
    isLoaded = true;
  }

  var config = INI.Read(Path.Combine(directory, "config.ini"));
  bool isSlowStartEnabled = bool.Parse(config["cyber"]["slowStart"]);
  bool isOpenZoneRotationEnabled = bool.Parse(config["cyber"]["openZoneRotation"]);
  bool isMomentumEnabled = bool.Parse(config["global"]["momentum"]);

  var SonicParams = Reflection.GetDataInfo<SonicParameters.Root>("player_common");
  if (SonicParams.pData == null) return;

  if (isSlowStartEnabled) {
    RFL_SET_PARAM(SonicParams, cyberspace.modePackage.speed.normal.initial, 5f);
    RFL_SET_PARAM(SonicParams, cyberspace.modePackage.speed.normal2.initial, 5f);
  }

  if (isOpenZoneRotationEnabled) {
    RFL_SET_PARAM(SonicParams, cyberspace.modePackage.rotation.rotationForceDecayRate, 0f);
    RFL_SET_PARAM(SonicParams, cyberspace.modePackage.rotation.rotationForceDecayMax, 0f);
  }

  if (isMomentumEnabled)
    RFL_SET_PARAM(SonicParams, cyberspace.modePackage.speed.maxGravityAccele, 10f);
}

Code "Cyber Space 2D Changes" by "cali_burrito"
//
  #include "Reflection" noemit

  #lib "Reflection"
  #lib "SonicParameters"
  #lib "INI"

  using System.IO;

  static bool isLoaded = false;
  static string directory;
//
{
  if (!isLoaded) {
    directory = Path.Combine(Directory.GetCurrentDirectory(), "Mods", "Refined Physics");
    isLoaded = true;
  }

  var config = INI.Read(Path.Combine(directory, "config.ini"));
  bool isFasterBoostEnabled = bool.Parse(config["cyberSV"]["fasterBoost"]);
  bool isMomentumEnabled = bool.Parse(config["global"]["momentum"]);

  var SonicParams = Reflection.GetDataInfo<SonicParameters.Root>("player_common");
  if (SonicParams.pData == null) return;

  if (isFasterBoostEnabled) {
    RFL_SET_PARAM(SonicParams, cyberspaceSV.modePackage.speed.boost.initial, 30f);
    RFL_SET_PARAM(SonicParams, cyberspaceSV.modePackage.speed.boost.min, 20f);
    RFL_SET_PARAM(SonicParams, cyberspaceSV.modePackage.speed.boost.max, 30f);
    RFL_SET_PARAM(SonicParams, cyberspaceSV.modePackage.speed.boost2.initial, 30f);
    RFL_SET_PARAM(SonicParams, cyberspaceSV.modePackage.speed.boost2.min, 20f);
    RFL_SET_PARAM(SonicParams, cyberspaceSV.modePackage.speed.boost2.max, 30f);
  }

  if (isMomentumEnabled)
    RFL_SET_PARAM(SonicParams, cyberspaceSV.spinBoost.maxGravityDecele, 0f);
}
