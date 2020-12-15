#include<iostream>
#include<string.h>
using namespace std;
class BaiTap{
	private:
		//giao tiep
		string Question_GiaoTiep[1]={
			"What your name"
		};
		string Anwer_GiaoTiep[1] = {
			"My Name AI"
		};
		string Question_GiaoTiep1[1]={
			"How you feel to day"
		};
		string Anwer_GiaoTiep1[1] = {
			"I'm fine, Thank's and you"
		};
		// Anhviet
		string Question_AV[1] = {
			"Hello"
		};
		string Anwer_AV[1] = {
			"Xin chao"
		};
		
		// VietAnh 
		string Question_VA[1] = {
			"Xin Chao"
		};
		string Anwer_VA[1] = {
			"Hello"
		};
		
		string mess;
	public:
		void Menu();
		void GiaoTiep();
		void AV();
		void VA();
		int Thoat();
};
// menu
void BaiTap::Menu(){
	cout<<"=============================================";
	cout<<"\t\n 	  BAI TAP TRI TUE NHAN TAO \n";
	cout<<"\t 	NHOM ......\n";
	cout<<"\t 	AI CHAT BOT \n";
	cout<<"--------------------------------------------\n";
	cout<<"\t 	   MENU \n";
	cout<<"--------------------------------------------";
	cout<<"\n1. Communication";
	cout<<"\n2. English-Vietnamese Dictionary";
	cout<<"\n3. Vietnamese English Dictionary";
	cout<<"\n4. Thoat";
	cout<<"\n------------------------------------------";
	
	int n; // chon tinh nang
	do
	{
		cout<<"\n Please select a feature: ";cin>>n;
		switch(n)
		{
			case 1:
				GiaoTiep();
				break;
			case 2:
				AV();
				break;
			case 3:
				VA();
				break;
			case 4:
				Thoat();
				break;
		}
	} while (n>4);
	cout<<"=============================================";
}
// GiaoTiep
void BaiTap::GiaoTiep(){
	cout<<"------------------------------------------";
	cout<<"\n    Hello You, Welcome to Communication ";
	do
	{
		cout<<"\n You: ";getline(cin,mess);
		if(mess=="")
		{
			cout<<"\n AI: Please, Enter your question";
		}
		else if(mess==Question_GiaoTiep[0])
		{
			cout<<" AI: "<<Anwer_GiaoTiep[0];
		}
		else if(mess==Question_GiaoTiep1[0])
		{
			cout<<" AI: "<<Anwer_GiaoTiep1[0];
		}
		else if (mess=="Exit" || mess =="exit")
		{
			Menu();
		}
		else 
		{
			cout<<" AI: Oh no !!! Sorry, I also don't answer";
		}
	}while(mess != "Exit" || mess!= "exit");//neu dieu kien 
	cout<<"------------------------------------------";
}
void BaiTap::AV(){
	cout<<"\n Hello You, Welcome to English-Vietnamese Dictionary";
	do
	{
		cout<<"\n You: ";getline(cin,mess);
		if(mess=="")
		{
			cout<<"\n AI: Please, Enter your question";
		}
		else if(mess==Question_AV[0])
		{
			cout<<" AI: "<<Anwer_AV[0];
		}
		else if (mess=="Exit")
		{
			Menu();
		}
		else 
		{
			cout<<" AI: Oh no !!! Sorry, I also don't answer";
		}
	}while (mess!="Exit" || mess!="exit");
}
void BaiTap::VA()
{
	cout<<"\n Hello You, Welcome to Vietnamese English Dictionary";
	do
	{
		cout<<"\n You: ";getline(cin,mess);
		if(mess=="")
		{
			cout<<"\n AI : Please, Enter your question";
		}
		else if(mess==Question_VA[0])
		{
			cout<<" AI: "<<Anwer_VA[0];
		}
		else if (mess=="Exit")
		{
			Menu();
		}
		else 
		{
			cout<<" AI: Oh no !!! Sorry, I also don't answer";
		}
	}while (mess!="Exit" || mess!="exit");
}
int BaiTap::Thoat(){
	return 0;
}
int main()
{
	BaiTap bt = BaiTap();
	bt.Menu();
//test
