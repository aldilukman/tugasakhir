#include <UIPEthernet.h>
#include <LiquidCrystal_PCF8574.h> //mengincludekan library LCD
#include <Wire.h>
#include <SoftwareSerial.h>
SoftwareSerial mySerial(3, 4);
//lcd
LiquidCrystal_PCF8574 lcd(0x27);
EthernetServer server = EthernetServer(12345);
void setup() {

  Serial.begin(9600);
  mySerial.begin(9600);
  //lcdInit();
  //setLCD1("Hello");
  //setLCD2("Tugas Akhir");
  delay(2000);
  //setupConnection();
  //setLCD2("Connection OK");
  //mySerial.println(F("I_")); //init
}


void loop() {
  /*
  if (EthernetClient user = server.available())
  {
    int size;
    
    while ((size = user.available()) > 0)
    {
      uint8_t* msg = (uint8_t*)malloc(size);
      //memset(msg, 0, size+1);
      size = user.read(msg, size);
      String dataReceive = (char*)msg;
      //setLCD1(dataReceive);
      free(msg);
      setLCD2(dataReceive);
      String dataCode = getValue(dataReceive, '_', 0);
      String dataValue = getValue(dataReceive, '_', 1);
      
      if (dataCode == "R") {
        //enroll(dataValue.toInt());
        mySerial.println(dataReceive);
        setLCD1("Register data");
      }
      //open gate
      //open_name
      else if (dataCode == "O") {
        setLCD1(dataValue);
        setLCD2("Open Gate");

        //setLCD2(dataValue);
        //open gate
        //digitalWrite(2, HIGH);
        delay(3000);
        //digitalWrite(2, LOW);
        setLCD2("Close Gate");
      }
      //delete user
      //delete_id
      else if (dataCode == "d") {
        //deleteFingerprint(dataValue.toInt());
      }
      //delete all
      //deleteall_deleteall
      else if (dataCode == "D") {
        //deleteall
        //emptyData();
        setLCD1("Delete data");
        setLCD2("Try Delete data");
      }

    }
    user.stop();
  }
  if (mySerial.available()) {
    String dataReceive = mySerial.readStringUntil('\n');
    setLCD2(dataReceive);
    String dataCode = getValue(dataReceive, '_', 0);
    String dataValue = getValue(dataReceive, '_', 1);
    if (dataCode == "P") {
      setLCD2(dataValue);
    }
    //open gate
    //masuk
    else if (dataCode == "M") {
      setLCD1("Checking Data");
      sendData(dataValue);
    }
    else if (dataCode == "S"){
      setLCD1("Sukses");
      sendData("Sukses");
    }
    
    else if (dataCode == "B3"){
        digitalWrite(2, HIGH);
        delay(500);
        digitalWrite(2, LOW);
        delay(500);
        digitalWrite(2, HIGH);
        delay(500);
        digitalWrite(2, LOW);
        delay(500);
        digitalWrite(2, HIGH);
        delay(500);
        digitalWrite(2, LOW);
        delay(500);
    }
  }*/
}
