#include<iostream>
#include<Windows.h>

int main(int argc,char *argv[],char *envp[])
{
	if(argc<=1)return 0; 
	
	for(int i = 1;i<argc;i++)
	{
		char c[256];
		char dot = '.';
		strcpy(c,argv[i]);
		char* dpos = strrchr(c,dot);
		strcpy(dpos,".ogg");
	
		int error = rename(argv[i],c);
		if(error)
		{
			std::cout<<"Failed.";
			system("pause");
		}
	}
} 
