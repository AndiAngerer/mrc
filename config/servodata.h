#pragma once

#include <Arduino.h>
#include "servoports.h"

// pinNumber, maxAngularVel degree/sec, calibMin, calibMax, angleDegMin, angleDegMax, home position
// minAngle must be less than maxAngle. To flip the direction of rotation do:
// {minFreq <-> maxFreq, minAngle * -1 <-> maxAngle * -1}
const float servoConfig[6][7] = {
    { pin_servo_0,  150*DEG_TO_RAD, 2360.00, 540.00, 60.57*DEG_TO_RAD, -110.57*DEG_TO_RAD, 0 },
    { pin_servo_1,  150*DEG_TO_RAD, 2400.00, 1000.00, 79.22*DEG_TO_RAD, -60.57*DEG_TO_RAD, 0 },
    { pin_servo_2,  150*DEG_TO_RAD, 600.00, 2330.00, -17.57*DEG_TO_RAD, 63.41*DEG_TO_RAD, 0 },
    { pin_servo_3,  150*DEG_TO_RAD, 2309.00, 559.00, 65.57*DEG_TO_RAD, -170.31*DEG_TO_RAD, 0 },
    { pin_servo_4,  150*DEG_TO_RAD, 2173.00, 806.00, -50.02*DEG_TO_RAD, 120.03*DEG_TO_RAD, 0 },
    { pin_servo_5,  150*DEG_TO_RAD, 566.00, 2306.00, -87.15*DEG_TO_RAD, 90.15*DEG_TO_RAD, 0 }
};