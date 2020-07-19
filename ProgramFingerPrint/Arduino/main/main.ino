#include <SPI.h>
#include <UIPEthernet.h>
#include <LiquidCrystal_PCF8574.h> //mengincludekan library LCD
#include <Wire.h>
#include <Adafruit_Fingerprint.h>
#include <SoftwareSerial.h>
SoftwareSerial mySerial(3, 4);
Adafruit_Fingerprint finger = Adafruit_Fingerprint(&mySerial);
//lcd
LiquidCrystal_PCF8574 lcd(0x27);
EthernetServer server = EthernetServer(12345);
void setup() {
  Serial.begin(9600);

  lcdInit();
  
  setupConnection();
  setupFingerPrint();

}


void loop() {
  
  getFingerprintID();
  if (EthernetClient user = server.available())
  {
    int size;
    while ((size = user.available()) > 0)
    {
      uint8_t* msg = (uint8_t*)malloc(size);
      size = user.read(msg, size);
      String dataReceive = (char*)msg;
      free(msg);
      String dataCode = getValue(dataReceive,'_',0);
      String dataValue = getValue(dataReceive,'_',1);
      //register user
      //register_id
      if(dataCode == "R"){
        enroll(dataValue.toInt());
      }
      //open gate
      //open_name
      else if(dataCode == "O"){
        setLCD1("Open Gate");
        
        setLCD2(dataValue);
        //open gate
        digitalWrite(2, HIGH);
        delay(3000);
        digitalWrite(2, LOW);
      }
      //delete user
      //delete_id
      else if(dataCode == "d"){
        deleteFingerprint(dataValue.toInt());
      }
      //delete all
      //deleteall_deleteall
      else if(dataCode == "da"){
        emptyData();
      }
      
    }
    user.stop();
  }

}
