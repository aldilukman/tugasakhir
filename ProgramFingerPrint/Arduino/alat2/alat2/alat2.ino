#include <Adafruit_Fingerprint.h>

SoftwareSerial mySerial(4, 3);
Adafruit_Fingerprint finger = Adafruit_Fingerprint(&mySerial);

void setup() {
  // put your setup code here, to run once:
  Serial.begin(9600);
  setupFingerPrint();
}

void loop() {
  // put your main code here, to run repeatedly:
  getFingerprintID();
  if(Serial.available() > 0) {
    String dataReceive = Serial.readStringUntil('\n');
    String dataCode = dataReceive.substring(0,dataReceive.indexOf('_'));
    String dataValue = dataReceive.substring(dataReceive.indexOf('_')+1);
    if (dataCode == "R") {
      enroll(dataValue.toInt());
    }else if(dataCode == "I"){
      setupFingerPrint();
    }
    else if (dataCode == "d") {
      deleteFingerprint(dataValue.toInt());
    }
    else if (dataCode == "D") {
      emptyData();
    }
  }
}
