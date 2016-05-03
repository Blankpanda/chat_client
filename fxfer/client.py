import socket
import sys



def main():
    s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    s.connect(("192.168.1.11",9999))
    
    f=open("test.txt", "rb")
    l = f.read(1024)
    
    while(l):
        s.send(l)
        l = f.readline(1024)
        
    s.close()
if __name__ == '__main__':
    main()
 
