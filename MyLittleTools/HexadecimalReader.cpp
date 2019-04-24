#include<iostream>
#include<conio.h>
#include<Windows.h>
#include<stack>

using namespace std; 

bool pause = true;
DWORD address = 0;
BYTE data[8] = {0};
int count = 8;

//TODO Undo Recorder
struct UndoNode
{
	int count;
	int row;	
};
stack<UndoNode> recorder = stack<UndoNode>();

void popRecorder(){
	if(recorder.top().row == 1)recorder.pop();
	else recorder.top().row--;
	
	address-=count;
}

HANDLE handle; 

int main(int argc,char *argv[],char *envp[]){
	//initial
	handle = GetStdHandle(STD_OUTPUT_HANDLE);
	
	
	//Get File Name 
	char *c;
	cout<<"File name:";
	
	if(argc>1)
	{
		c = argv[1];	
		cout<<c<<endl;
	}
	else
	{
		cin>>c;
	}
	
	//Open File 
	FILE *f = fopen(c,"rb");
	if(f==NULL)
	{
		printf("file doesn't exist!");
		system("pause");
		return 0;
	}
	
	//print Header 
	cout
	<<"©°©¤©¤©¤©¤©¤©¤©¤©¤©¤©Ð©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤\t©Ð©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤\t©Ð©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©´\n"
	<<"©¦ Address ©¦ Hexadecimal Code       \t©¦ Char value      \t©¦ Int value   ©¦\n"
	<<"©À©¤©¤©¤©¤©¤©¤©¤©¤©¤©à©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤\t©à©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤\t©à©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©È\n";
	
	
	while(true){
		//read data
		int dataCountToRead = count;
		int p=0;
		while(!feof(f)&&dataCountToRead>0)
		{
			data[p] = fgetc(f);
			p++;
			dataCountToRead--;
		}
		while(p<8)
		{
			data[p] = 0;
			p++;
		}
		count-=dataCountToRead;
		
		//display
		if(count == 0)
		{
			//the last row
			cout
			<<"©¸©¤©¤©¤©¤©¤©¤©¤©¤©¤©Ø©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤\t©Ø©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤\t©Ø©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¤©¼\n"; 
		}
		else
		{
			//address
			printf("©¦ %08X©¦ ",address);
			
			//data
			for(int i=0;i<count;i++)printf("%02X ",data[i]);
			for(int i=0;i<8-count;i++)printf("   ");
			printf("\t©¦ ");
			
			//char value
			int space = 16-2*count;
			for(int i=0;i<count;i++)
			{
				BYTE j = data[i];
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
					printf("©¸L");
					break;
				case 0x11:
					printf("©¸1");
					break;
				case 0x12:
					printf("©¸2");
					break;
				case 0x13:
					printf("©¸3");
					break;
				case 0x14:
					printf("©¸4");
					break;
				default:
					if(j>=128)
					{
						printf("%c",j);
						space++;
					}
					else printf("%-2c",j);
					break;
				}
			}
			for(int i=0;i<space;i++)printf(" ");
			printf("   \t©¦ ");
			
			//int value
			printf("%12ld©¦",*((long*)data));
		}
		
		//record
		if(recorder.empty()||recorder.top().count!=count)recorder.push({count,1});
		else recorder.top().row++;
		address+=count;
		
		//Control
		if(pause||_kbhit())
		{
			int g = getch();
			if(g == '\b')
			{
				pause = true;
				cout<<"\r";
				fseek(f,-count,SEEK_CUR);
				popRecorder();
				
				if(recorder.empty())
				{
					cout<<'\a';
					continue;
				}
				
				cout<<" ";
				CONSOLE_SCREEN_BUFFER_INFO info = {};
				GetConsoleScreenBufferInfo(handle,&info);
				SetConsoleCursorPosition(handle,{0,info.dwCursorPosition.Y-1});
				count = recorder.top().count;
				fseek(f,-count,SEEK_CUR);
				
				popRecorder();
			}
			else if(count == 0)break;
			else switch(g)
			{
			case 32://space
				pause = !pause;
				
				cout<<endl;
				break;
			case 27://ESC
				count = 0;
				cout<<endl;
				break;
			case '1':
			case '2':
			case '3':
			case '4':
			case '5':
			case '6':
			case '7':
			case '8':	
				fseek(f,-count,SEEK_CUR);
				popRecorder();
				
				count = g-'0';
				cout<<'\r';	
				break;
			default://next row
				cout<<endl;
				break;
			}
							
		}
		else
		{
			cout<<endl;
		}
	}

	cout<<"Reading end, total size: "<<address<<"Byte"<<endl;
	
	system("pause");
	return 0;
} 
