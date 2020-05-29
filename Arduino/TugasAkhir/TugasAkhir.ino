#include <PZEM004Tv30.h>
#include <WiFi.h>
#include <WiFiClient.h>
#include <WiFiAP.h>

/* Hardware Serial3 is only available on certain boards.
 * For example the Arduino MEGA 2560
*/
PZEM004Tv30 pzem(&Serial2);


#define LED_BUILTIN 2   // Set the GPIO pin where you connected your test LED or comment this line out if your dev board has a built-in LED

// Set these to your desired credentials.
const char *ssid = "Esp8266";
const char *password = "12345678";
String dataConcat;
WiFiServer server(12345);

void setup() {
  pinMode(LED_BUILTIN, OUTPUT);
  digitalWrite(LED_BUILTIN, LOW);
  Serial.begin(115200);

   WiFi.softAP(ssid, password);
  IPAddress myIP = WiFi.softAPIP();
  Serial.print("AP IP address: ");
  Serial.println(myIP);
  server.begin();

  Serial.println("Server started");
}

void loop() {
  
    const uint16_t port = 1337;
    const char * host = "192.168.4.2"; // ip or dns
    
    Serial.print("Connecting to ");
    Serial.println(host);

    // Use WiFiClient class to create TCP connections
    WiFiClient client;
    if (client.connect(host, port)) {
        
        dataConcat = "";
        float voltage = pzem.voltage();
        if(!isnan(voltage)){
            Serial.print("Voltage: "); Serial.print(voltage); Serial.println("V");
        } else {
            Serial.println("Error reading voltage");
        }
    
        float current = pzem.current();
        if(!isnan(current)){
            Serial.print("Current: "); Serial.print(current); Serial.println("A");
        } else {
            Serial.println("Error reading current");
        }
    
        float power = pzem.power();
        if(!isnan(power)){
            Serial.print("Power: "); Serial.print(power); Serial.println("W");
        } else {
            Serial.println("Error reading power");
        }
    
        float energy = pzem.energy();
        if(!isnan(energy)){
            Serial.print("Energy: "); Serial.print(energy,3); Serial.println("kWh");
        } else {
            Serial.println("Error reading energy");
        }
    
        float frequency = pzem.frequency();
        if(!isnan(frequency)){
            Serial.print("Frequency: "); Serial.print(frequency, 1); Serial.println("Hz");
        } else {
            Serial.println("Error reading frequency");
        }
    
        float pf = pzem.pf();
        if(!isnan(pf)){
            Serial.print("PF: "); Serial.println(pf);
        } else {
            Serial.println("Error reading power factor");
        }
    
        Serial.println();
    
    
        dataConcat += String(voltage);
        dataConcat += "_";
        dataConcat += String(current);
        dataConcat += "_";
        dataConcat += String(power);
        dataConcat += "_";
        dataConcat += String(energy,3);
        dataConcat += "_";
        dataConcat += String(frequency,1);
        dataConcat += "_";
        dataConcat += String(pf);
        client.print(dataConcat);
        
        
        delay(1000);
        if (client.available() > 0)
        {
          //read back one line from the server
          String line = client.readStringUntil('\r');
          if(line == "nyala"){
            digitalWrite(LED_BUILTIN, HIGH);
          }else if (line == "mati"){
            digitalWrite(LED_BUILTIN, LOW);
          }
          Serial.println(line);
        }
        
        client.stop(); 
    }
    else{
        Serial.println("Connection failed.");
        Serial.println("Waiting 2 seconds before retrying...");
        delay(1000);
    }
    
    
    
}
