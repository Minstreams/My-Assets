#include<iostream>
#include<conio.h>
#include<string.h>

int main(int argc,char *argv[],char *envp[])
{
	if(argc<=1)return 0;//only able to open files
	
	char c[256] = "HexadecimalReader.exe ";
	strcat(c,argv[1]);
	//std::cout<<c;
	system(c); 
	
	std::cout<<"\nagent end...\n";
	system("pause");
	return 0;
} 
#include<Wingdi.h>
