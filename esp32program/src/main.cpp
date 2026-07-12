#include <Arduino.h>
#include <WiFi.h>
#include <DHT11.h>

// const char* ssid = "Test"; // Имя сети
// const char* password = "aboba1337"; // Пароль
DHT11 dht11(4); // Инициализация датчика DHT11 на пине 4

// Optional: declare any custom functions here
int myFunction(int, int);
int temperature = 0;
int humidity = 0;
void setup() {
  Serial.begin(115200);
  // WiFi.softAP(ssid, password);
  // IPAddress IP = WiFi.softAPIP();
  // Serial.print("IP-адрес точки доступа: ");
  // Serial.println(IP);
}

void loop() {
    String requestCommand = Serial.readStringUntil('\n');
    requestCommand.trim();
    
    if(requestCommand == "GET TEMP") 
    {
      // Attempt to read the temperature and humidity values from the DHT11 sensor.
      int result = dht11.readTemperatureHumidity(temperature, humidity);
      dht11.setDelay(100);
      // Check the results of the readings.
      // If the reading is successful, print the temperature and humidity values.
      // If there are errors, print the appropriate error messages.
      if (result == 0) {
          Serial.print("Temp=");
          Serial.print(temperature);
          Serial.print(" Hum=");
          Serial.println(humidity);
      } else {
          // Print error message based on the error code.
          Serial.println(DHT11::getErrorString(result));
      }
    }
    // requestCommand = ""; // Clear the command after processing
}

int myFunction(int x, int y) {
  return x + y;
}