
char v0d, v1d, vel;

void setup()
{
  Serial.begin(115200);
}

void loop()
{
  Get_Instruccion();
}



int Get_Instruccion()
{
  if(Serial.available())
  {
      switch(Serial.read())
      {
         case 'r':
           Receive_Param();
         break; 
         
         case 's':
          // Sair();
         break;
      }
  }
}
/*
void Sair()
{

}
*/
void Receive_Param()
{

    v0d = Serial.read();
    Serial.write(v0d);
    v1d = Serial.read();
    Serial.write(v0d);
    vel = Serial.read();
    Serial.write(v0d);
}
