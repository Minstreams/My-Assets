#include<iostream>
#include<conio.h>

bool pause = true;
unsigned int ad = 0;
		
int main(int argc,char *argv[],char *envp[]){
	char *c;
	std::cout<<"File name:";
	if(argc>1)
	{
		c = argv[1];	
		std::cout<<c<<std::endl;
	}
	else
	{
		std::cin>>c;
	}

	unsigned char intList[8] ={0};
	
	FILE *f = fopen(c,"rb");
	if(f!=NULL)
	{
		std::cout
		<<"©°©¤©¤©¤©¤©¤©¤©¤©¤©¤©Ð©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤\t©Ð©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤\t©Ð©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©´\n"
		<<"©¦ Address ©¦ Hexadecimal Code       \t©¦ Char value      \t©¦ Int value               ©¦\n"
		<<"©À©¤©¤©¤©¤©¤©¤©¤©¤©¤©à©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤\t©à©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤\t©à©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©È\n";
		while(fread(intList,sizeof(char),8,f)>0)
		{
			
			printf("©¦ %08X©¦ ",ad);
			for(int i=0;i<8;i++)printf("%02X ",intList[i]);
			printf("\t©¦ ");
			for(int i=0;i<8;i++)
			{
				unsigned char j = intList[i];
				switch(j)
				{
				case '\n':
					printf("\\n");
					break;
				case '\r':
					printf("\\r");
					break;
				case '\b':
					printf("\\b");
					break;
				case '\t':
					printf("\\t");
					break;
				case '\a':
					printf("\\a");
					break;
				case 0xff:
					printf("¡ö");
					break;
				case 0x10:
					printf("DL");
					break;
				case 0x11:
					printf("D1");
					break;
				case 0x12:
					printf("D2");
					break;
				case 0x13:
					printf("D3");
					break;
				case 0x14:
					printf("D4");
					break;
				default:
					if(i<7&&j>=128){
						unsigned char t = intList[i+1];
						
						switch(t)
						{
						case '\n':
						case '\r':
						case '\b':
						case '\t':
						case '\a':
							printf("¡ö");
							break;
						default:
							printf("%c%c",j,t);
							break;
						}
					}
					else printf("%-2c",j);
					break;	
				}	
			}
			printf("   \t©¦ ");
			
			printf("%10u \t%10u©¦",*((unsigned int*)(intList)+0),*((unsigned int*)(intList)+1));

			printf("\n");
					
						
			//press esc to exit
			if(pause||_kbhit())
			{
				int g = getch();
				if(g==32)pause=pause?false:true;
				if(g==27)break;
			}
			ad+=8;
		}
		std::cout
		<<"©¸©¤©¤©¤©¤©¤©¤©¤©¤©¤©Ø©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤\t©Ø©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤\t©Ø©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¼\n"; 
		fclose(f);
	}
	else
	{
		printf("file doesn't exist!");
	}
	
	system("pause");
	return 0;
} 
