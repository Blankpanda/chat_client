import socket
import sys
#  Directly reference the os module
sys.path.append(r"C:\Program Files (x86)\IronPython 2.7\Lib")
import os
import codecs
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
            
            l = handler.recv(4096) # buffer to hold what command we are
            
            if l == b"PUT":
                do_put(handler,res_folder_path) # PUT command
            if l == b"GET":
                do_get(handler,res_folder_path)
            if l == "LIST":
                do_list(handler,res_folder_path)
            
                
            
        handler.close()

    listener.close()


def do_put(handler, res_folder_path):
    stop_code = str(300)
    buff = "" # used when writting to the file literally not sure why this works

    # the first information we are getting is the name of the file and its extension
    l = handler.recv(4096) # I doubt it wont be very big so I dont think we'll need to loop to parse this
    file_name, file_exten = get_file_name_exten(str(l))
    
    f = open(res_folder_path + "\\" + file_name + '.' +  file_exten, 'wb') # create a new file
    
    l = handler.recv(4096)
    while(l):
        print("recieving {0}".format(l))

        buff = buff +  l # append data and recieve more
        buff = buff.strip(str(300)) # hack to remove the stop code
        if stop_code in l:
            break
        
        l = handler.recv(4096)
        
        if not l: break
    
    f.write(buff) # write the contents of the file
    f.close()
    print("done writing new file.")
    return None

def do_get(handler,res_folder_path):
    stop_code = 300

    filename = handler.recv(4096) # first were going to get the name of the file
    filename = filename.decode('utf-8')
    # search the file list to see if that file exists.
    if filename in os.listdir(res_folder_path):
        # send the file that were looking for
        
        f = codecs.open(res_folder_path + '\\' + filename, 'r', encoding='utf8',errors='replace')
        line = f.read(4096)    
        while(line):
            handler.send(bytes(line,'utf8')) 
            line = f.readline(4096)
     
    handler.send(bytes(str(stop_code), 'utf8'))
    
    return None
    
        
def do_list(handler,res_folder_path):
    file_list = os.listdir(res_folder_path)

    # build a string with the list, delimiting the list using a new line
    send_str = ""
    for fna36me in file_list:
        send_str = send_str + fname + '\n'

    handler.send(send_str)
    
def get_file_name_exten(filename):
    # simply split by the perion
    name, exten = filename.split('.')
    return name, exten

# for testing compliation, this program is invoked through IronPython in Visual Studio
if __name__ == '__main__':
    Start("192.168.1.10","car","it",7777,10,r"..\res")
