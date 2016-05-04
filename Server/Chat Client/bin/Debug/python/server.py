import socket
import sys
#  Directly reference the os module
sys.path.append(r"C:\Program Files (x86)\IronPython 2.7\Lib")
import os
from threading import Thread


def Start(ip_address, server_name, server_password,server_port_number,server_backlog, res_folder_path):
    
    # start of by converting the numeric arguments into integers
    server_port_number = int(server_port_number)
    server_backlog = int(server_backlog)
    
    print("started {0} file server under port {1}".format(server_name, server_port_number))

    # create, bind and listen based on server settings
    listener = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    listener.bind((ip_address,server_port_number))
    listener.listen(server_backlog)

    while True:
        handler, address = listener.accept() # handler = handle requests , address = the remote end point

        print("{0} has connected.".format(address[0]))
        
        while(True):
            l = handler.recv(1024) # buffer to hold what command we are
            if l == b"PUSH":
                get_push(handler,res_folder_path)
                print('out')
            if l == "GET":
                pass
            if l == "LIST":
                pass
            else:
                continue
                
              
           
            # if len(l) >= 1:                            
            #     print("recieving {0}".format(l))
            # f.write(l) # write what we've recieved
            # l = handler.recv(1024) # continue to get more
            # if len(l) == 0:
            #     f.close()

        handler.close()

    listener.close()


def get_push(handler, res_folder_path):
    file_index = 0
    f = open(res_folder_path + "\\" + "file_" + str(file_index) + ".txt", 'wb') # os.join
    file_index = file_index + 1

    buf = ""

    l = handler.recv(1024)
    while(l):
        print("recieving {0}".format(l))
        buf = buf +  l
        l = handler.recv(1024)
        if not l: break

    f.write(buf)
    f.close()
    
    return None
    
if __name__ == '__main__':
    Start("192.168.5.184","car","it",7777,10,r"..\res")
