#define MACADDRESS 0x00,0x01,0x02,0x03,0x04,0x05
#define MYIPADDR 192,168,1,6
#define MYIPMASK 255,255,255,0
#define MYDNS 192,168,1,1
#define MYGW 192,168,1,1

#include <SPI.h>
#include <UIPEthernet.h>
#include <LiquidCrystal_PCF8574.h> //mengincludekan library LCD
#include <Wire.h>
#include <SoftwareSerial.h>
SoftwareSerial mySerial(4, 3);
//lcd
LiquidCrystal_PCF8574 lcd(0x27);
EthernetServer server = EthernetServer(12345);
void setup() {

  Serial.begin(9600);
  mySerial.begin(9600);
  lcdInit();
  setLCD1("Hello");
  setLCD2("Tugas Akhir");
  delay(2000);
  setupConnection();
  setLCD2("Connection OK");

}


void loop() {
  setLCD2("Waiting Finger");
  //getFingerprintID();
  if (EthernetClient user = server.available())
  {
    int size;
    setLCD2("Receive Data");
    while ((size = user.available()) > 0)
    {
      uint8_t* msg = (uint8_t*)malloc(size);
      size = user.read(msg, size);
      String dataReceive = (char*)msg;
      free(msg);
      String dataCode = getValue(dataReceive, '_', 0);
      String dataValue = getValue(dataReceive, '_', 1);
      //register user
      //register_id
      if (dataCode == "R") {
        //enroll(dataValue.toInt());
        setLCD1("Register data");
      }
      //open gate
      //open_name
      else if (dataCode == "O") {
        setLCD1("Hello "+ dataValue);
        setLCD1("Open Gate");

        setLCD2(dataValue);
        //open gate
        digitalWrite(2, HIGH);
        delay(3000);
        digitalWrite(2, LOW);
      }
      //delete user
      //delete_id
      else if (dataCode == "d") {
        //deleteFingerprint(dataValue.toInt());
      }
      //delete all
      //deleteall_deleteall
      else if (dataCode == "da") {
        //deleteall
        //emptyData();
        setLCD1("Delete data");
        setLCD2("Try Delete data");
      }

    }
    user.stop();
  }
  if (mySerial.available()) {
    String dataReceive = mySerial.readString();
    String dataCode = getValue(dataReceive, '_', 0);
    String dataValue = getValue(dataReceive, '_', 1);
    if (dataCode == "P") {
      //enroll(dataValue.toInt());
      setLCD2(dataValue);
    }
    //open gate
    //masuk
    else if (dataCode == "M") {
      setLCD1("Checking Data");
      sendData(dataValue);
    }
  }

}
