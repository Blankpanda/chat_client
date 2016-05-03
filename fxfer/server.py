import socket
import sys

def main():
    s = socket.socket()
    s.bind(("localhost",9999))
    s.listen(10)

    while True:
        sc, address = s.accept()

        print address
        i=1
        f = open('file_' + str(i)+".txt","wb")
        i = i + 1
        while(True):
            l = sc.recv(1024)
            while(1):
                f.write(l)
                l = sc.recv(1024)
        f.close()

        sc.close()
        
    s.close

if __name__ == '__main__':
    main()
