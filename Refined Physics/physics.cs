// Main purpose of this document is to get C# to show up in the languages section lol

Code "Decreased Deceleration" by "mploythai" does "Decrease Sonic's deceleration on ground and in air, allowing him to gain speed naturally without boosting and to prevent instant momentum loss after boosting/power boosting."
//
  #include "ReflectionHelpers" noemit

  #lib "SonicParameters"

  static float deceleForce = 5f;
//
{
  var SonicParams = Reflection.GetDataInfo<SonicParameters.Root>("player_common");
  if (SonicParams.pData == null) return;

  RFL_SET_PARAM(SonicParams, forwardView.modePackage.speed.decele.force, deceleForce);
  RFL_SET_PARAM(SonicParams, forwardView.modePackage.speed.decele.force2, deceleForce);
}

Code "Decreased Falling Deceleration" by "mploythai" does "Decrease Sonic's deceleration in air, allowing him to retain his forward momentum while falling uncurled."
//
  #include "ReflectionHelpers" noemit

  #lib "SonicParameters"
//
{
  var SonicParams = Reflection.GetDataInfo<SonicParameters.Root>("player_common");
  if (SonicParams.pData == null) return;

  RFL_SET_PARAM(SonicParams, forwardView.modePackage.fall.deceleForce, 10f);
  RFL_SET_PARAM(SonicParams, forwardView.modePackage.fall.overSpeedDeceleForce, 20f);
}

Code "Momentum Rails" by "mploythai" does "Make rails more momentum-based, allowing Sonic to gain/decrease speed when going up/down rails. Additionally, the minimum rail grinding speed has been decreased, and maximum rail speed is increased."
//
  #include "ReflectionHelpers" noemit

  #lib "SonicParameters"
//
{
  var SonicParams = Reflection.GetDataInfo<SonicParameters.Root>("player_common");
  if (SonicParams.pData == null) return;

  RFL_SET_PARAM(SonicParams, forwardView.modePackage.grind.deceleForce, 5f);
  RFL_SET_PARAM(SonicParams, forwardView.modePackage.grind.limitSpeedMin, 10f);
}

Code "Generations-Styled Air Boost" by "mploythai" does "Make the air boost act similar to the one in Sonic Generations."
//
  #include "ReflectionHelpers" noemit

  #lib "SonicParameters"
  #lib "BlackboardItem"
//
{
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

Code "Brake Easing for Wall Running" by "mploythai" does "Make Sonic ease into a stop when letting go of the boost button while wall running."
//
  #include "ReflectionHelpers" noemit

  #lib "SonicParameters"
//
{
  var SonicParams = Reflection.GetDataInfo<SonicParameters.Root>("player_common");
  if (SonicParams.pData == null) return;

  RFL_SET_PARAM(SonicParams, forwardView.modePackage.wallmove.brake, 25f);
}

Code "Decreased Minimum Slide Speed" by "mploythai" does "Decreased the minimum sliding speed."
//
  #include "ReflectionHelpers" noemit

  #lib "SonicParameters"
//
{
  var SonicParams = Reflection.GetDataInfo<SonicParameters.Root>("player_common");
  if (SonicParams.pData == null) return;

  RFL_SET_PARAM(SonicParams, forwardView.modePackage.sliding.minSpeed, 5f);
}

Code "Softer Ring Dash Brake" by "mploythai" does "Prevent Sonic from coming to an instant stop after ring dashing."
//
  #include "ReflectionHelpers" noemit

  #lib "SonicParameters"
//
{
  var SonicParams = Reflection.GetDataInfo<SonicParameters.Root>("player_common");
  if (SonicParams.pData == null) return;

  RFL_SET_PARAM(SonicParams, forwardView.lightDash.brake, 75f);
}

Code "Easier Incline Jumps" by "mploythai" does "Makes incline jumps easier."
//
  #include "ReflectionHelpers" noemit

  #lib "SonicParameters"
//
{
  var SonicParams = Reflection.GetDataInfo<SonicParameters.Root>("player_common");
  if (SonicParams.pData == null) return;

  RFL_SET_PARAM(SonicParams, forwardView.modePackage.jumpSpeed.limitUpSpeed, 35f);
}

Code "Better Spin Boost" by "mploythai" does
/*
  Inspired by Weezley's Legacy Spin Dash Mod!
  - Spin boost is now momentum-based
  - Spin boosting without charging retains Sonic's prior forward momentum, making it act like a normal roll
  - Spin charging on the ground determines how fast the spin boost is after releasing the charge
  - In the air, the longer the player holds the spin charge button, the faster the resulting spin boost will be once Sonic hits the ground
  - Flinging off a platform while spin boosting is now much smoother (I think)
*/
//
  #include "ReflectionHelpers" noemit

  #lib "Sonic"
  #lib "SonicParameters"
  #lib "BlackboardItem"
  #lib "MathHelpers"

  static float chargeCount = 0f;
  static float deceleForce = 5f;
//
{
  var dashVelocity = Sonic.Kinematics.GetHorizontalMagnitude();
  var SonicParams = Reflection.GetDataInfo<SonicParameters.Root>("player_common");
  if (SonicParams.pData == null) return;

  var minSpeed = BlackboardItem.GetRingCount() != SonicParams.pData->forwardView.modePackage.common.capacityRings ? 32.5f : 40f;
  var maxSpeed = BlackboardItem.GetRingCount() != SonicParams.pData->forwardView.modePackage.common.capacityRings ? 65f : 80f;
  var addedSpeed = BlackboardItem.GetRingCount() != SonicParams.pData->forwardView.modePackage.common.capacityRings ? 6.5f : 10f;

  if (Sonic.IsGrounded() && Sonic.State.GetCurrentStateID() == Sonic.StateID.StateSpinBoostCharge) {
    if (Sonic.Input.IsPressed(Sonic.PlayerActionType.PlayerSonicboom))
      chargeCount += 1f;
    
    dashVelocity = MathHelpers.Clamp((minSpeed - addedSpeed) + (chargeCount * addedSpeed), minSpeed, maxSpeed);
  } else if (!Sonic.IsGrounded()) {
    // potentially add forward momentum code here in the future

    if (Sonic.Input.IsDown(Sonic.PlayerActionType.PlayerSonicboom) && Sonic.State.GetCurrentStateID() == Sonic.StateID.StateSpinBoostCharge) {
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

Code "Faster Boost Consumption" by "mploythai" does "Since Sonic can retain his momentum better in Refined Physics, there's no need for him to boost for a long time. To balance this, I've increased the consumption rate for the gauge, but also increased the recovery rate."
//
  #include "ReflectionHelpers" noemit

  #lib "SonicParameters"
//
{
  var SonicParams = Reflection.GetDataInfo<SonicParameters.Root>("player_common");
  if (SonicParams.pData == null) return;

  RFL_SET_PARAM(SonicParams, forwardView.modePackage.boost.consumptionRate, Sonic.State.GetCurrentStateID() != Sonic.StateID.StateSpinBoost ? 100f : 50f);
  RFL_SET_PARAM(SonicParams, forwardView.modePackage.boost.recoveryRate, 25f);
  RFL_SET_PARAM(SonicParams, forwardView.modePackage.boost.infinityBoostTime, 120f);
}

Code "Challenging Parry" by "mploythai" does "Makes the parry faster."
//
  #include "ReflectionHelpers" noemit

  #lib "SonicParameters"
//
{
  var SonicParams = Reflection.GetDataInfo<SonicParameters.Root>("player_common");
  if (SonicParams.pData == null) return;

  RFL_SET_PARAM(SonicParams, forwardView.modePackage.parry.minRecieveTime, 0.15f);
  RFL_SET_PARAM(SonicParams, forwardView.modePackage.parry.maxRecieveTime, 0.3f);
  RFL_SET_PARAM(SonicParams, forwardView.modePackage.parry.justEffectTime2, 1f);
}