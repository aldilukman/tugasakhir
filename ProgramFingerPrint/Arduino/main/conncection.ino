

void setupConnection() {
  uint8_t mac[6] = {MACADDRESS};
uint8_t myIP[4] = {MYIPADDR};
uint8_t myMASK[4] = {MYIPMASK};
uint8_t myDNS[4] = {MYDNS};
uint8_t myGW[4] = {MYGW};
  Ethernet.begin(mac,myIP,myDNS,myGW,myMASK);
  server.begin();
}
void sendData(String numberFF) {
  EthernetClient client;
  if (client.connect(IPAddress(192, 168, 1, 1), 12346))
  {
    client.println(numberFF);
    client.stop();
  }
  else
  {
    setLCD1("Server Not Connect");
    setLCD2("Server Not Connect");
  }
}

String getValue(String data, char separator, int index)
{
  int found = 0;
  int strIndex[] = {0, -1};
  int maxIndex = data.length()-1;

  for(int i=0; i<=maxIndex && found<=index; i++){
    if(data.charAt(i)==separator || i==maxIndex){
        found++;
        strIndex[0] = strIndex[1]+1;
        strIndex[1] = (i == maxIndex) ? i+1 : i;
    }
  }

  return found>index ? data.substring(strIndex[0], strIndex[1]) : "";
}
