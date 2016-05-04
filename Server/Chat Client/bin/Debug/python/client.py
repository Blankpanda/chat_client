import socket
import sys


command_list = ['GET','PUSH','LIST', 'EXIT','HELP']
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
    sender.connect((server_address,server_port ))

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
        except Exception:
            print("")

        
    # f=open("test.txt", "rb")
    # l = f.read(1024)
    
    # while(l):
    #     sender.send(l)
    #     l = f.readline(1024)
        
    # sender.close()

# hacky but it will do for now
def run_command(command):
    print(command)
    if command == "HELP":
        c_help()
        return None
    if command == "PUSH":
        push()
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
    print("The following commands are: {0} \n {1} \n {2} \n {3}, {4}".format(
        "help - show this dialog.",
        "push - push a file to the server.",
        "get  - get a file from the server.",
        "list - list files on the server.",
        "exit - exit out of the program."
    ))


def push():
    sender.send(b"PUSH")
    try:
        path = input("Enter the path of the file that you would like to send: ")
        f = open(path, 'rb')

        line = f.read(1024)

        while(l):
            sender.send(buf)
            line = f.readline(1024)
            
        except Exception:
            print("bla") # todo error checking
            return None

    

def get():
    pass

def c_list():
    pass

if __name__ == '__main__':
    main()
 
