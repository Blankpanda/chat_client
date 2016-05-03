import socket
import sys
#  Directly reference the os module
sys.path.append(r"..\..\..\..\packages\IronPython.2.7.5\lib")
# import os
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

        i = 0
        f = open(res_folder_path + "\\" + 'file_' + str(i),"wb") # I dont have the os module
        i = i + 1
        
        while(True):
            l = handler.recv(1024) # buffer to hold what where recieving
            if len(l) >= 1:                            
                print("recieving {0}".format(l))
            f.write(l) # write what we've recieved
            l = handler.recv(1024) # continue to get more
            if len(l) == 0:
                f.close()

        handler.close()

    listener.close()

if __name__ == '__main__':
    Start("192.168.1.10","car","it",7777,10)
