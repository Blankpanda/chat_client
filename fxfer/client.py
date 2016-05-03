import socket
import sys



def main():
    s = socket.socket()
    s.connect(("localhost",9999))
    f=open(("test.txt", "rb"))

    l = f.read(1024):
    while(l):
        s.send(l)
        l = f.readline(1024)
        
    s.close()
if __name__ == '__main__':
    main()
