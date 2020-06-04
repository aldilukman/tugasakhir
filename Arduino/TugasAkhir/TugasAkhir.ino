#include <PZEM004Tv30.h>
#include <WiFi.h>
#include <WiFiClient.h>
#include <PubSubClient.h>
/* Hardware Serial3 is only available on certain boards.
 * For example the Arduino MEGA 2560
*/
PZEM004Tv30 pzem(&Serial2);


#define LED_BUILTIN 2   // Set the GPIO pin where you connected your test LED or comment this line out if your dev board has a built-in LED

// Set these to your desired credentials.
const char *ssid = "Esp8266";
const char *password = "12345678";
const char* mqtt_server = "broker.hivemq.com";


WiFiClient espClient;
PubSubClient client(espClient);
long lastMsg = 0;
char msg[255];
int value = 0;

String dataConcat;

void callback(char* topic, byte* payload, unsigned int length) {
  String myString = String(topic);
  Serial.print(myString);

  Serial.print("Message arrived [");
  Serial.print(topic);
  Serial.print("] ");
  for (int i = 0; i < length; i++) {
    Serial.print((char)payload[i]);
  }
  Serial.println();
    if ((char)payload[0] == '1') {
     digitalWrite(LED_BUILTIN, HIGH);  // Turn the LED on by making the voltage HIGH
  } else {
     digitalWrite(LED_BUILTIN, LOW);  // Turn the LED off by making the voltage LOW
  }
  
  

}

void reconnect() {
  // Loop until we're reconnected
  while (!client.connected()) {
    Serial.print("Attempting MQTT connection...");
    // Create a random client ID
    String clientId = "ESP8266Client-";
    clientId += String(random(0xffff), HEX);
    // Attempt to connect
    if (client.connect(clientId.c_str())) {
      Serial.println("connected");
      
      client.subscribe("tugasakhir/control");
    } else {
      Serial.print("failed, rc=");
      Serial.print(client.state());
      Serial.println(" try again in 5 seconds");
      // Wait 5 seconds before retrying
      delay(5000);
    }
  }
}


void setup() {
  pinMode(LED_BUILTIN, OUTPUT);
  digitalWrite(LED_BUILTIN, LOW);
  Serial.begin(115200);
  Serial.println("Starting connecting WiFi.");
  delay(10);
  WiFi.begin(ssid, password);
  while (WiFi.status() != WL_CONNECTED) {
    delay(500);
    Serial.print(".");
  }
  Serial.println("WiFi connected");
  Serial.println("IP address: ");
  Serial.println(WiFi.localIP());

  client.setServer(mqtt_server, 1883);
  client.setCallback(callback);
  
  
}

void loop() {
  
    
        
    
  if (!client.connected()) {
    reconnect();
  }
  client.loop();

  long now = millis();
    if (now - lastMsg > 1000) {
        lastMsg = now;
        ++value;
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

        // Length (with one extra character for the null terminator)
        int str_len = dataConcat.length() + 1; 
         
        // Prepare the character array (the buffer) 
        char char_array[str_len];
         
        // Copy it over 
        dataConcat.toCharArray(char_array, str_len);
        
        client.publish("tugasakhir/status",char_array );
    }
      
    
    
}
