const int vib_motor_pin1 = 9;
const int vib_motor_pin2 = 11;
const int buttonPin = 6;

int buttonState = 0;

int v = 0;

unsigned long time;

void setup() {
  // put your setup code here, to run once:
  Serial.begin(9600);

  pinMode(vib_motor_pin1, OUTPUT);
  pinMode(vib_motor_pin2, OUTPUT);
}

void loop() {

  buttonState = digitalRead(buttonPin);

  if(buttonState == LOW){
    v = 0;
  }
  if(buttonState == HIGH){
    time = millis();
    v = 1;
  }
  Serial.print(v);
  Serial.println();
  if(buttonState == HIGH){
    delay(1400);
  }
  
  if(buttonState == LOW){
    digitalWrite(vib_motor_pin1, LOW); 
    digitalWrite(vib_motor_pin2, LOW); 
  } else if(buttonState == HIGH){
    time = millis() - time;
    digitalWrite(vib_motor_pin1, HIGH);
    digitalWrite(vib_motor_pin2, HIGH);
    delay(400);
  }
}
