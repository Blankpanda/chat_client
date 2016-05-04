import socket
import sys


command_list = ['GET','PUT','LIST', 'EXIT','HELP']
sender = socket.socket(socket.AF_INET, socket.SOCK_STREAM)

def main():

    try:
        args = sys.argv
        server_socket = args[1]
        server_password = args[2] 
    except Exception:
        print("Invalid number of arguments. try: \n python client.py server_ip:server_port server_password")
        sys.exit(0)

    if len(args) != 3:
        print("Invalid number of arguments. try: \n python client.py server_ip server_password")
        sys.exit(0)


    # lousy check to see if its a socket
    if not ":" in server_socket:
        print("Inavlid socket. try IPaddress:Portnumber")
        sys.exit(0)
        
    # split the server socket into an IP address and a PORT number
    server_address,server_port = server_socket.split(':')

    # if its a integer were good to go, if it throws a exepction its not an integer
    try:
        server_port = int(server_port)
    except Exception:
        print("Invalid port number. use a integer.")
        sys.exit(0)
    
    # make sure that server_address is a ip address, if not exit the application
    try:
        socket.inet_aton(server_address) # throws an error if the address supplied is not a IP address
    except socket.error:
        print("Invalid IP address, try X.X.X.X format")
        sys.exit(0)
    
    # setup socket
    try:
        sender.connect((server_address,server_port ))
    except Exception as e:
        print("Failed to connect to the server. Is it online?")
        print(str(e))


    # REEPL for the user to enter in commands to the server
    
    while True:
        # parse command
        try:
            user_in = input("> ")
            user_in = user_in.upper()
            if user_in in command_list:
                run_command(user_in)
            else:
                print("Invalid command, type 'help' for help dialog.")
        except Exception as e:
            raise e
        

# hacky but it will do for now
def run_command(command):
    if command == "HELP":
        c_help()
        return None
    if command == "PUT":
        put()
        return None
    elif command == "GET":
        get()
        return None
    elif command == "LIST":
        c_list()
        return None
    elif command == "EXIT":
        c_exit()
        return None
    else:
        print("Invalid command, type 'help' for help dialog.")
        return None
        

def c_exit():
    sender.close()
    sys.exit(0)

def c_help():
    print("The following commands are: \n {0} \n {1} \n {2} \n {3} \n {4}".format(
        "help - show this dialog.",
        "put - push a file to the server.",
        "get  - get a file from the server.",
        "list - list files on the server.",
        "exit - exit out of the program."
    ))


def put():
    stop_code = 300
    sender.send(b"PUT") # the command that going to be used
    
    path = input("Enter the path of the file that you would like to send: ")
    f = open(path, 'rb')

    filename = get_file_from_path(path)
    sender.send(bytes(filename, 'ASCII')) # send the name of the file, so the server know how to stor it
    
    line = f.read(1024) # read the first line and send it (essentailly we want o to initalize line)
    while(line): # start sending the rest
        sender.send(b'' + line)
        line = f.readline(1024)
    
    # send a stop code because were done sending
    sender.send(bytes(str(stop_code), 'ASCII'))

    return None

def get():
    stop_code = str(300)
    buff = ""

    sender.send(b'GET')

    filename = input("Enter the name of the file that you would like to get: ")

    # todo: check if the file exist1s
    sender.send(bytes(filename,'ASCII')) # send the file name that were looking for

    f = open(filename,'wb')

    data = sender.recv(1024)
    while(data):
        buff = buff + data.decode('ASCII')
        buff = buff.strip(str(300))
        if stop_code in data.decode('ASCII'):
            break

        data = sender.recv(1024)
        
        if not data: break

    print(buff)
    f.write(bytes(buff,'ASCII'))
    f.close()
    print("done writing new file.")
    return None

def c_list():
    stop_code = 300
    sender.send(b'LIST') # send the command name

    reply = sender.recv(1024) # wait for a reply

    print(reply.decode('ASCII')) # write out the reply
    
    


def get_file_from_path(filename):
    if '\\' not in filename:
        return filename
        
    
    filename = filename[::-1]

    first_dir_count = 0
    for char in filename:
        if char == '\\':
            break
        first_dir_count += 1

    filename = filename[:first_dir_count]
    filename = filename[::-1]
    return filename       
     
if __name__ == '__main__':
    main()
 
