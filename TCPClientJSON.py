import socket
import json

serverName = "localhost"
serverPort = 1010  

clientSocket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
clientSocket.connect((serverName, serverPort))

method = input("Enter method (Random/Add/Subtract): ")
num1 = int(input("Enter first number: "))
num2 = int(input("Enter second number: "))

request = {"Method": method, "Num1": num1, "Num2": num2}
requestJson = json.dumps(request)

clientSocket.sendall((requestJson + "\n").encode())  # Send JSON request
responseJson = clientSocket.recv(1024).decode()  # Receive JSON response

response = json.loads(responseJson)
print("Server response:", response)

clientSocket.close()