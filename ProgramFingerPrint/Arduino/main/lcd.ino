/**
  Converts a color code to its numerical value.

  @param colorCode color code to convert.
  @return the numerical value of the color code.
*/
/*
void lcdInit(){

 Wire.begin();
 Wire.beginTransmission(0x27);
 Wire.endTransmission();
 
 lcd.begin(16, 2); //prosedur pemanggilan LCD
 lcd.setBacklight(255);
}

/**
  Converts a color code to its numerical value.

  @param colorCode color code to convert.
  @return the numerical value of the color code.
*/
/*
void setLCD1(String dataDisplay){
  //lcd.clear();
  lcd.setCursor(00,00); //set pada baris 1 dan kolom 1
  lcd.print("                ");
  lcd.setCursor(00,00); //set pada baris 1 dan kolom 1
  lcd.print(dataDisplay);
}

/**
  Converts a color code to its numerical value.

  @param colorCode color code to convert.
  @return the numerical value of the color code.
*/
/*
void setLCD2(String dataDisplay2){
  lcd.setCursor(00,1); //set pada baris 2 dan kolom 1
  lcd.print("                ");
  lcd.setCursor(00,1); //set pada baris 2 dan kolom 1
  lcd.print(dataDisplay2);
}
/**
  Converts a color code to its numerical value.

  @param colorCode color code to convert.
  @return the numerical value of the color code.
*/

void hapusLCD(){
  lcd.clear();
}
