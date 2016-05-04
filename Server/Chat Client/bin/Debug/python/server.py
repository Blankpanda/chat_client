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
            
            if l == b"PUT":
                do_put(handler,res_folder_path) # PUT command
            if l == "GET":
                pass
            if l == "LIST":
                pass
            else:
                pass
            
        handler.close()

    listener.close()


def do_put(handler, res_folder_path):
    stop_code = str(300)
    buff = "" # used when writting to the file

    # the first information we are getting is the name of the file and its extension
    l = handler.recv(1024) # I doubt it wont be very big so I dont think we'll need to loop to parse this
    file_name, file_exten = get_file_name_exten(str(l))
    
    f = open(res_folder_path + "\\" + file_name + '.' +  file_exten, 'wb') # create a new file
    
    l = handler.recv(1024)
    while(l):
        print("recieving {0}".format(l))

        buff = buff +  l # append data and recieve more
        buff = buff.strip(str(300)) # hack to remove the stop code
        if stop_code in l:
            break
        
        l = handler.recv(1024)
        
        if not l: break
    
    f.write(buff) # write the contents of the file
    f.close()
    print("done writing new file.")
    return None

def do_get(handler,res_folder_path):
    pass

def do_list(handler,res_folder_path):
    pass


def get_file_name_exten(filename):
    # simply split by the perion
    name, exten = filename.split('.')
    return name, exten
# for testing compliation, this program is invoked through IronPython in Visual Studio
if __name__ == '__main__':
    Start("192.168.1.10","car","it",7777,10,r"..\res")
