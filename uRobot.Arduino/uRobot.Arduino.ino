
// The motors pin
int M1_F = 3;
int M1_B = 5;
int M2_F = 6;
int M2_B = 7;


void setup() {
	// Set the motors pins as outputs
	pinMode(M1_F, OUTPUT);
	pinMode(M1_B, OUTPUT);
	pinMode(M2_F, OUTPUT);
	pinMode(M2_B, OUTPUT);

	// prepare the serial communcaion at 9600 baud rate speed
	Serial.begin(9600);

}

// the loop function runs over and over again until power down or reset
void loop() {
	
	if (Serial.available())
	{
		char data = Serial.read();
		if (data == 'F')
		{
			moveForward();
		}
		else if(data == 'B')
		{
			moveBackward();
		}
		else if (data == 'R')
		{
			moveRight();
		}
		else if (data == 'L')
		{
			moveLeft();
		}
		else if(data == 'S')
		{
			stop();
		}
	}
}

void moveForward()
{
	analogWrite(M1_F, 150);
	analogWrite(M1_B, 0);
	analogWrite(M2_F, 150);
	analogWrite(M2_B, 0);
}


void moveBackward()
{
	analogWrite(M1_F, 150);
	analogWrite(M1_B, 0);
	analogWrite(M2_F, 150);
	analogWrite(M2_B, 0);
}


void moveRight()
{
	analogWrite(M1_F, 0);
	analogWrite(M1_B, 150);
	analogWrite(M2_F, 150);
	analogWrite(M2_B, 0);
}


void moveLeft()
{
	analogWrite(M1_F, 150);
	analogWrite(M1_B, 0);
	analogWrite(M2_F, 0);
	analogWrite(M2_B, 150);
}


void stop()
{
	analogWrite(M1_F, 0);
	analogWrite(M1_B, 0);
	analogWrite(M2_F, 0);
	analogWrite(M2_B, 0);
}
