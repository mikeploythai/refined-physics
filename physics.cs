// Main purpose of this document is to get C# to show up in the languages section lol

Code "Decreased Ground Deceleration" by "mploythai"
//
  #include "ReflectionHelpers" noemit

  #lib "SonicParameters"
  #lib "INI"

  using System.IO;

  static float deceleForce = 5f;
  static bool isLoaded = false;
  static string directory;
//
{
  if (!isLoaded) {
    directory = Path.Combine(Directory.GetCurrentDirectory(), "Mods", "Refined Physics");
    isLoaded = true;
  }

  var config = INI.Read(Path.Combine(directory, "config.ini"));
  var isEnabled = bool.Parse(config["openZone"]["decGroundDecele"]);

  if (isEnabled) {
    var SonicParams = Reflection.GetDataInfo<SonicParameters.Root>("player_common");
    if (SonicParams.pData == null) return;

    RFL_SET_PARAM(SonicParams, forwardView.modePackage.speed.decele.force, deceleForce);
    RFL_SET_PARAM(SonicParams, forwardView.modePackage.speed.decele.force2, deceleForce);
  }
}

Code "Decreased Falling Deceleration" by "mploythai"
//
  #include "ReflectionHelpers" noemit

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
  var isEnabled = bool.Parse(config["openZone"]["decFallDecele"]);

  if (isEnabled) {
    var SonicParams = Reflection.GetDataInfo<SonicParameters.Root>("player_common");
    if (SonicParams.pData == null) return;

    RFL_SET_PARAM(SonicParams, forwardView.modePackage.fall.deceleForce, 10f);
    RFL_SET_PARAM(SonicParams, forwardView.modePackage.fall.overSpeedDeceleForce, 20f);
  }
}

Code "Momentum Rails" by "mploythai"
//
  #include "ReflectionHelpers" noemit

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
  var isEnabled = bool.Parse(config["openZone"]["momentumRails"]);

  if (isEnabled) {
    var SonicParams = Reflection.GetDataInfo<SonicParameters.Root>("player_common");
    if (SonicParams.pData == null) return;

    RFL_SET_PARAM(SonicParams, forwardView.modePackage.grind.deceleForce, 5f);
    RFL_SET_PARAM(SonicParams, forwardView.modePackage.grind.limitSpeedMin, 10f);
  }
}

Code "Generations-Styled Air Boost" by "mploythai"
//
  #include "ReflectionHelpers" noemit

  #lib "SonicParameters"
  #lib "BlackboardItem"
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
  var isEnabled = bool.Parse(config["openZone"]["gensAirBoost"]);

  if (isEnabled) {
    var SonicParams = Reflection.GetDataInfo<SonicParameters.Root>("player_common");
    if (SonicParams.pData == null) return;

    var minSpeed = BlackboardItem.GetRingCount() != SonicParams.pData->forwardView.modePackage.common.capacityRings ? 35f : 55f;
    var maxSpeed = BlackboardItem.GetRingCount() != SonicParams.pData->forwardView.modePackage.common.capacityRings ? 55f : 75f;

    RFL_SET_PARAM(SonicParams, forwardView.modePackage.airboost.startHSpeed, minSpeed);
    RFL_SET_PARAM(SonicParams, forwardView.modePackage.airboost.startHSpeedMax, maxSpeed);
    RFL_SET_PARAM(SonicParams, forwardView.modePackage.airboost.minHSpeed, minSpeed);
    RFL_SET_PARAM(SonicParams, forwardView.modePackage.airboost.minHSpeedMax, maxSpeed);
    RFL_SET_PARAM(SonicParams, forwardView.modePackage.airboost.brakeTime, 0f);
    RFL_SET_PARAM(SonicParams, forwardView.modePackage.airboost.minKeepTime, 0f);
    RFL_SET_PARAM(SonicParams, forwardView.modePackage.airboost.maxKeepTime, 0f);
    RFL_SET_PARAM(SonicParams, forwardView.modePackage.airboost.maxTime, 0f);
  }
}

Code "Brake Easing for Wall Running" by "mploythai"
//
  #include "ReflectionHelpers" noemit

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
  var isEnabled = bool.Parse(config["openZone"]["wallBrake"]);

  if (isEnabled) {
    var SonicParams = Reflection.GetDataInfo<SonicParameters.Root>("player_common");
    if (SonicParams.pData == null) return;

    RFL_SET_PARAM(SonicParams, forwardView.modePackage.wallmove.brake, 25f);
  }
}

Code "Decreased Minimum Slide Speed" by "mploythai"
//
  #include "ReflectionHelpers" noemit

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
  var isEnabled = bool.Parse(config["openZone"]["minSlide"]);

  if (isEnabled) {
    var SonicParams = Reflection.GetDataInfo<SonicParameters.Root>("player_common");
    if (SonicParams.pData == null) return;

    RFL_SET_PARAM(SonicParams, forwardView.modePackage.sliding.minSpeed, 5f);
  }
}

Code "Softer Light Speed Dash Brake" by "mploythai"
//
  #include "ReflectionHelpers" noemit

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
  var isEnabled = bool.Parse(config["openZone"]["softLSDBrake"]);

  if (isEnabled) {
    var SonicParams = Reflection.GetDataInfo<SonicParameters.Root>("player_common");
    if (SonicParams.pData == null) return;

    RFL_SET_PARAM(SonicParams, forwardView.lightDash.brake, 75f);
  }
}

Code "Easier Incline Jumps" by "mploythai"
//
  #include "ReflectionHelpers" noemit

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
  var isEnabled = bool.Parse(config["openZone"]["ezJump"]);

  if (isEnabled) {
    var SonicParams = Reflection.GetDataInfo<SonicParameters.Root>("player_common");
    if (SonicParams.pData == null) return;

    RFL_SET_PARAM(SonicParams, forwardView.modePackage.jumpSpeed.limitUpSpeed, 35f);
  }
}

Code "Better Spin Boost" by "mploythai"
//
  #include "ReflectionHelpers" noemit

  #lib "Sonic"
  #lib "SonicParameters"
  #lib "BlackboardItem"
  #lib "MathHelpers"
   #lib "INI"

  using System.IO;

  static float chargeCount = 0f;
  static float deceleForce = 5f;
  static bool isLoaded = false;
  static string directory;
//
{
  if (!isLoaded) {
    directory = Path.Combine(Directory.GetCurrentDirectory(), "Mods", "Refined Physics");
    isLoaded = true;
  }

  var config = INI.Read(Path.Combine(directory, "config.ini"));
  var isEnabled = bool.Parse(config["openZone"]["betterSpinBoost"]);

  if (isEnabled) {
    var dashVelocity = Sonic.Kinematics.GetHorizontalMagnitude();
    var SonicParams = Reflection.GetDataInfo<SonicParameters.Root>("player_common");
    if (SonicParams.pData == null) return;

    var minSpeed = BlackboardItem.GetRingCount() != SonicParams.pData->forwardView.modePackage.common.capacityRings ? 35f : 55f;
    var maxSpeed = BlackboardItem.GetRingCount() != SonicParams.pData->forwardView.modePackage.common.capacityRings ? 55f : 75f;

    if (Sonic.IsGrounded() && Sonic.State.GetCurrentStateID() == Sonic.StateID.StateSpinBoostCharge) {
      if (Sonic.Input.IsPressed(Sonic.PlayerActionType.PlayerSonicboom))
        chargeCount += 1f;
      
      dashVelocity = MathHelpers.Clamp((minSpeed - 5) + (chargeCount * 5), minSpeed, maxSpeed);
    } else if (!Sonic.IsGrounded()) {
      // potentially add forward momentum code here in the future

      if (Sonic.State.GetCurrentStateID() == Sonic.StateID.StateSpinBoostCharge) {
        chargeCount += 1f;
        dashVelocity = MathHelpers.Clamp(chargeCount / 1.25f, minSpeed, maxSpeed);
      }
    } else if (Sonic.State.GetPreviousStateID() != Sonic.StateID.StateSpinBoostCharge && Sonic.State.GetCurrentStateID() != Sonic.StateID.StateSpinBoost) {
      dashVelocity = Sonic.Kinematics.GetHorizontalMagnitude();
      chargeCount = 0f;
    }

    RFL_SET_PARAM(SonicParams, forwardView.spinBoost.initialRunTime, 0.2f);
    RFL_SET_PARAM(SonicParams, forwardView.spinBoost.speedBoost.initialSpeed, dashVelocity);
    RFL_SET_PARAM(SonicParams, forwardView.spinBoost.speedBoost.maxSpeed, 3f);
    RFL_SET_PARAM(SonicParams, forwardView.spinBoost.speedBoost.minTurnSpeed, 30f);
    RFL_SET_PARAM(SonicParams, forwardView.spinBoost.speedBoost.decele.force, deceleForce);
    RFL_SET_PARAM(SonicParams, forwardView.spinBoost.speedBoost.decele.force2, deceleForce);
    RFL_SET_PARAM(SonicParams, forwardView.spinBoost.deceleNeutralMin.force, 750f);
    RFL_SET_PARAM(SonicParams, forwardView.spinBoost.deceleNeutralMax.force, 1000f);
  }
}

Code "Faster Boost Consumption" by "mploythai"
//
  #include "ReflectionHelpers" noemit

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
  var isEnabled = bool.Parse(config["openZone"]["fasterBoostGauge"]);

  if (isEnabled) {
    var SonicParams = Reflection.GetDataInfo<SonicParameters.Root>("player_common");
    if (SonicParams.pData == null) return;

    RFL_SET_PARAM(SonicParams, forwardView.modePackage.boost.consumptionRate, Sonic.State.GetCurrentStateID() != Sonic.StateID.StateSpinBoost ? 100f : 25f);
    RFL_SET_PARAM(SonicParams, forwardView.modePackage.boost.recoveryRate, 25f);
    RFL_SET_PARAM(SonicParams, forwardView.modePackage.boost.infinityBoostTime, 120f);
  }
}

Code "Harder Parry" by "mploythai"
//
  #include "ReflectionHelpers" noemit

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
  var isEnabled = bool.Parse(config["openZone"]["harderParry"]);

  if (isEnabled) {
    var SonicParams = Reflection.GetDataInfo<SonicParameters.Root>("player_common");
    if (SonicParams.pData == null) return;

    RFL_SET_PARAM(SonicParams, forwardView.modePackage.parry.minRecieveTime, 0.15f);
    RFL_SET_PARAM(SonicParams, forwardView.modePackage.parry.maxRecieveTime, 0.3f);
    RFL_SET_PARAM(SonicParams, forwardView.modePackage.parry.justEffectTime2, 1f);
  }
}
