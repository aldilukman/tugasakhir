
void setupFingerPrint()  
{

  // set the data rate for the sensor serial port
  finger.begin(57600);
  
  if (finger.verifyPassword()) {
    setLCD1("Found Sensor");
  } else {
    setLCD1("Not Found Sensor");
    while (1) { delay(1); }
  }
}
void enroll(int idNumber)                     // run over and over again
{
  while (!  getFingerprintEnroll(idNumber) );
}

uint8_t getFingerprintEnroll(int id) {
  
  int p = -1;
  setLCD1("Register");
  setLCD2("Insert Finger ");
  while (p != FINGERPRINT_OK) {
    p = finger.getImage();
    switch (p) {
    case FINGERPRINT_OK:
      setLCD2("Image taken");
      break;
    case FINGERPRINT_NOFINGER:
      setLCD2("No Finger");
      break;
    default:
      setLCD2("Failed Read");
      break;
    }
  }

  // OK success!

  p = finger.image2Tz(1);
  switch (p) {
    case FINGERPRINT_OK:
      setLCD2("Image converted");
      break;
    
    default:
      setLCD2("Failed Read");
      return p;
  }
  
  setLCD2("Remove finger");
  delay(2000);
  p = 0;
  while (p != FINGERPRINT_NOFINGER) {
    p = finger.getImage();
  }
  setLCD2(id);
  p = -1;
  while (p != FINGERPRINT_OK) {
    p = finger.getImage();
    switch (p) {
    case FINGERPRINT_OK:
      setLCD2("Image taken");
      break;
    case FINGERPRINT_NOFINGER:
      setLCD2("Place finger");
      break;
    
    default:
      setLCD2("Failed read");
      break;
    }
  }

  // OK success!

  p = finger.image2Tz(2);
  switch (p) {
    case FINGERPRINT_OK:
      setLCD2("Image converted");
      break;
    default:
      setLCD2("Failed Read");
      return p;
  }
  
  // OK converted!
  setLCD2("Creating"); 
  
  p = finger.createModel();
  if (p == FINGERPRINT_OK) {
    setLCD2("Prints matched");
  } 
  else {
    setLCD2("Prints not matched");
    return p;
  }   
  
  p = finger.storeModel(id);
  if (p == FINGERPRINT_OK) {
    setLCD2("Stored!");
  } 
  else {
    setLCD2("Error writing");
    return p;
  }   
}

uint8_t getFingerprintID() {
  uint8_t p = finger.getImage();
  switch (p) {
    case FINGERPRINT_OK:
      setLCD2("Image taken");
      break;
    default:
      setLCD2("Failed Read");
      return p;
  }

  // OK success!

  p = finger.image2Tz();
  switch (p) {
    case FINGERPRINT_OK:
      setLCD2("Image converted");
      break;
    
    default:
      setLCD2("Failed Read");
      return p;
  }
  
  // OK converted!
  p = finger.fingerSearch();
  if (p == FINGERPRINT_OK) {
    setLCD2("Found a print match!");
  } else if (p == FINGERPRINT_NOTFOUND) {
    setLCD2("FF Not Found");
    return p;
  } else {
    setLCD2("Failed");
    return p;
  }   
  
  // found a match!
  setLCD1("Found ID #"); setLCD2(finger.fingerID); 
  sendData(finger.fingerID);
  return finger.fingerID;
}

void deleteFingerprint(uint8_t id) {
  uint8_t p = -1;
  setLCD1("Delete");
  p = finger.deleteModel(id);

  if (p == FINGERPRINT_OK) {
    setLCD1("Deleted");
    setLCD2(id);
  } else {
    setLCD2("Failed Delete");
  } 
}

void emptyData(){
  finger.emptyDatabase();
  setLCD1("Deleted All Data"); 
}
