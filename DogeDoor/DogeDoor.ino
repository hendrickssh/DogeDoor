#include <SoftwareSerial.h>
#include <AccelStepper.h>
#include <PIR.h>

#define rxPin 10 // connected to TX pin of HC05
#define txPin 11 // connected to RX pin of HC05
#define motorPin1  2     // IN1 on the ULN2003 driver 1
#define motorPin2  3     // IN2 on the ULN2003 driver 1
#define motorPin3  4     // IN3 on the ULN2003 driver 1
#define motorPin4  5     // IN4 on the ULN2003 driver 1
#define HALFSTEP 8
#define PIR_PIN_SIG  6   // IN on the motion sensor


SoftwareSerial mySerial(rxPin, txPin); // RX, TX
AccelStepper stepper1(HALFSTEP, motorPin1, motorPin3, motorPin2, motorPin4);
PIR pir(PIR_PIN_SIG);
char bt; 
int openDistance = 20000;
int closeDistance = 0;
bool locked = false;
bool isTurning = false;
int pirVal = 0;

void setup() 
{
  Serial.begin(9600);   
  mySerial.begin(9600);
  stepper1.setMaxSpeed(1000.0);
  stepper1.setAcceleration(100.0);
  stepper1.setSpeed(800);
  stepper1.setCurrentPosition(0);
}

void loop()
{
  stepper1.run();
  if(stepper1.currentPosition() > 0 && stepper1.currentPosition() < 20000){
    //door is opening
    isTurning = true;
  } else {
    //door is not moving
    isTurning = false;
    pirVal = pir.read();
  }

  if(!isTurning && mySerial.available()){
    //Door is not turning and bluetooth is connected
    while(mySerial.available())
    {
      Serial.println(bt);  
      ProcessInput(mySerial.read());
    }
    bt = '\n';
  } else if (!isTurning && pirVal == 1){
    //Door is not turing but bluetooth is not connected
    Serial.println("Openings from motion");
    ProcessInput('O');
  } else if( !isTurning && pirVal == 0){
    Serial.println("Closing from motion");
    ProcessInput('C');
  }
}

void ProcessInput(char input)
{
  if(input == 'O' || input == 'o')
    OpenDoor();
  else if(input == 'C' || input == 'c')
    CloseDoor();
  else if(input == 'L' || input == 'l')
    LockDoor();
}

void OpenDoor()
{
  if(!locked){
    Serial.println("Opening door");
    stepper1.moveTo(openDistance);
  }
  else
    Serial.println("Door is locked");
}

void CloseDoor()
{
  Serial.println("Closing door");
  stepper1.moveTo(closeDistance);
}

void LockDoor()
{
  Serial.println("Locking door");
  locked = !locked;
}

