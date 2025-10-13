// See https://aka.ms/new-console-template for more information





using System;
using System.Security.Cryptography.X509Certificates;
using System.Collections.Generic;
using System.Linq;



Character character = new Character();
Inventory inven = character.Inventory; // character 클래스 안에있는 Inventory 인스턴스
List<Item> items = inven.items; // character 클래스 안에 있는 Inventory 인스턴스 안에있는 item속성을 가지고있는 itme이란 이름의 리스트

Character Dcharacter = new Character();// character 값 초기화 할때 사용하는 캐릭터

Shop shop = new Shop();


int Item = 0;


bool[] equipped = { false, false, false };











void GameStart()
{

    Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.\r\n이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");
    Console.WriteLine();
    Console.WriteLine("1. 상태 보기\r\n2. 인벤토리\r\n3. 랜덤 모험\r\n4. 마을 순찰하기\r\n5. 훈련하기\r\n6. 상점"); // case "1" : 상태보기 "2" : 인벤토리 "3" : 랜덤 모험 "4": 마을 순찰하기 "5": 훈련하기
    Console.WriteLine();
    Console.WriteLine("원하시는 행동을 입력해주세요.");
    Console.Write(">>");
   


    while (true)
    {
        string insert = Console.ReadLine();

        switch (insert)
        {

            case "1": // 상태보기

                ShowStatus();
                break;

            case "2": // 인벤토리


                Console.WriteLine("인벤토리\n보유 중인 아이템을 관리할 수 있습니다.\n");
                Console.WriteLine();
                Console.WriteLine("[아이템 목록]");





                inven.ShowInventory(false , false);


                Console.WriteLine("1. 장착 관리");
                Console.WriteLine("0. 나가기");
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.WriteLine(">>");



                string insert2 = Console.ReadLine();
                if (insert2 == "0")
                {
                    Console.Clear();
                    GameStart();

                }
                else if (insert2 == "1")
                {

                    Console.WriteLine("인벤토리\n보유 중인 아이템을 관리할 수 있습니다.\n");
                    Console.WriteLine();
                    Console.WriteLine("[아이템 목록]");



                    inven.ShowInventory(true,true);




                    Console.WriteLine();
                    Console.WriteLine("0. 나가기");
                    Console.WriteLine();
                    Console.WriteLine("원하시는 행동을 입력해주세요.");
                    Console.WriteLine(">>");

                    while (true)
                    {
                        
                        Console.Write(">> ");
                        string input1 = Console.ReadLine();
                        if (input1 == "0")
                        {
                            Console.Clear();
                            GameStart();

                        }
                        inven.HandleInventoryInput(input1);
                        inven.ShowInventory(showIndex: true, showMark: true);
                    }








                }
                break;

            case "3": //랜덤 모험


                Console.WriteLine();
                Console.WriteLine("랜덤 모험");
                Console.WriteLine();
                Console.WriteLine();







                while (true)
                {




                    if (character.stamina - 10 < 0)
                    {
                        Console.WriteLine();
                        Console.WriteLine($"현재 스태미나 : {character.stamina}");
                        Console.WriteLine();
                        Console.WriteLine("스태미나가 부족합니다.");
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine("0. 나가기");
                        Console.WriteLine();
                        Console.WriteLine("원하시는 행동을 입력해주세요.");
                        Console.Write(">>");
                        string insert4 = Console.ReadLine();
                        if (insert4 == "0")
                        {


                            Console.Clear();
                            GameStart();


                            break;


                        }



                    }
                    else if (character.stamina - 10 >= 0)
                    {









                        character.stamina -= 10;
                        Console.WriteLine();
                        Console.WriteLine("스태미나 - 10을 썼습니다.");
                        Console.WriteLine();
                        Random Random = new Random();

                        int rand = Random.Next(0, 2);

                        if (rand == 0)
                        {



                            Console.WriteLine("\"몬스터 조우! 골드 500 획득\"");

                            character.gold += 500;





                        }
                        else if (rand == 1)
                        {



                            Console.WriteLine("\"아무 일도 일어나지 않았다\"");






                        }

                        Console.WriteLine();
                        Console.WriteLine($"현재 스태미나 : {character.stamina}");
                        Console.WriteLine();
                        Console.WriteLine("1. 다시하기");
                        Console.WriteLine();
                        Console.WriteLine("0. 나가기");
                        Console.WriteLine();
                        Console.WriteLine("원하시는 행동을 입력해주세요.");
                        Console.WriteLine();
                        Console.Write(">>");
                        string insert5 = Console.ReadLine();
                        if (insert5 == "0")
                        {

                            Console.Clear();
                            GameStart();


                            break;



                        }
                        else if (insert5 == "1")
                        { 
                        
                        
                        
                        
                        continue;
                        
                        
                        
                        
                        }




                    }

                }
                

                break;


            case "4": // 마을 순찰하기


                Console.WriteLine();
                Console.WriteLine("마을 순찰하기");
                Console.WriteLine();
                Console.WriteLine();
                int addGold = 0;




                while (true)
                {


                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine();


                    if (character.stamina - 5 < 0)
                    {
                        Console.WriteLine();
                        Console.WriteLine($"현재 스태미나 : {character.stamina}");
                        Console.WriteLine();
                        Console.WriteLine("스태미나가 부족합니다.");
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine("0. 나가기");
                        Console.WriteLine();
                        Console.WriteLine("원하시는 행동을 입력해주세요.");
                        Console.Write(">>");
                        string insert6 = Console.ReadLine();
                        if (insert6 == "0")
                        {


                            Console.Clear();
                            GameStart();


                            break;


                        }



                    }
                    else if (character.stamina - 5 >= 0)
                    {









                        character.stamina -= 5;
                        Console.WriteLine();
                        Console.WriteLine("스태미나 - 5을 썼습니다.");
                        Console.WriteLine();
                        Random Random = new Random();

                        

                        int rand = Random.Next(1,100);

                        if (rand <= 10)
                        {



                            Console.WriteLine("\"마을 아이들이 모여있다. 간식을 사줘볼까? -  500 G \"");

                            addGold -= 500;





                        }
                        else if (rand >10 && rand <= 20)
                        {



                            Console.WriteLine("\"촌장님을 만나서 심부름을 했다. +  2000 G \"");


                            addGold += 2000;



                        }
                        else if (rand >20 && rand <=40)
                        {



                            Console.WriteLine("\"길 읽은 사람을 안내해주었다. +  1000 G \"");
                            addGold += 1000;






                        }
                        else if (rand >40 && rand <= 70 )
                        {



                            Console.WriteLine("\"마을 주민과 인사를 나눴다. 선물을 받았다. +  500 G \"");

                            addGold += 500;




                        }
                        else if (rand > 70 && rand <= 100)
                        {



                            Console.WriteLine("\"아무 일도 일어나지 않았다\" +  0 G ");






                        }


                        character.gold += addGold;

                        Console.WriteLine();
                        Console.WriteLine($"\t\t총 얻은 골드 : {addGold}");
                        Console.WriteLine();
                        Console.WriteLine($"현재 스태미나 : {character.stamina}");
                        Console.WriteLine();
                        Console.WriteLine("1. 다시하기");
                        Console.WriteLine();
                        Console.WriteLine("0. 나가기");
                        Console.WriteLine();
                        Console.WriteLine("원하시는 행동을 입력해주세요.");
                        Console.WriteLine();
                        Console.Write(">>");
                        string insert7 = Console.ReadLine();
                        if (insert7 == "0")
                        {

                            Console.Clear();
                            GameStart();


                            break;



                        }
                        else if (insert7 == "1")
                        {




                            continue;




                        }




















                    }




                }












                break;
            case "5": // 훈련하기


                Console.WriteLine();
                Console.WriteLine("훈련 하기");
                Console.WriteLine();
                Console.WriteLine("스태미나 - 15");
                Console.WriteLine();
                int addEx = 0;

                Console.WriteLine("1. 훈련 하기");
                Console.WriteLine();
                Console.WriteLine("0. 나가기");
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.WriteLine();
                Console.Write(">>");
                string insert8 = Console.ReadLine();
                if (insert8 == "0")//나가기로 나감
                {

                    Console.Clear();
                    GameStart();


                    break;

                }
                else if (insert8 == "1")//훈련하기 들어감
                {



                    while (true) //훈련하기 한번들어가면 나가기로 나갈때까지 안나가짐
                    {


                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine();


                        if (character.stamina - 15 < 0)
                        {
                            Console.WriteLine();
                            Console.WriteLine("필요 스태미나 : 15");
                            Console.WriteLine();
                            Console.WriteLine($"현재 스태미나 : {character.stamina}");
                            Console.WriteLine();
                            Console.WriteLine("스태미나가 부족합니다.");
                            Console.WriteLine();
                            Console.WriteLine();
                            Console.WriteLine();
                            Console.WriteLine("0. 나가기");
                            Console.WriteLine();
                            Console.WriteLine("원하시는 행동을 입력해주세요.");
                            Console.Write(">>");
                            string insert9 = Console.ReadLine(); //나가기를 위해서 만듬
                            if (insert9 == "0") 
                            {


                                Console.Clear();
                                GameStart();


                                break;


                            }



                        }
                        else if (character.stamina - 15 >= 0)
                        {









                            character.stamina -= 15;
                            Console.WriteLine();
                            Console.WriteLine("스태미나 - 15을 썼습니다.");
                            Console.WriteLine();
                            Random Random = new Random();



                            int rand = Random.Next(1, 100);

                            if (rand <= 15)
                            {



                                Console.WriteLine("\"훈련이 잘 되었습니다! +  60 경험치 \"");

                                addEx += 60;





                            }
                            else if (rand > 15 && rand <= 75)
                            {



                                Console.WriteLine("\"오늘하루 열심히 훈련했습니다. +  2000 경험치 \"");


                                addEx += 40;



                            }
                            else if (rand > 75 && rand <= 100)
                            {



                                Console.WriteLine("\"하기 싫다... 훈련이... +  30 경험치 \"");
                                addEx += 30;






                            }
                            


                            character.ex += addEx;

                            Console.WriteLine();
                            Console.WriteLine($"\t\t총 얻은 경험치 : {addEx}");
                            Console.WriteLine();
                            Console.WriteLine($"현재 스태미나 : {character.stamina}");
                            Console.WriteLine();
                            Console.WriteLine("1. 다시하기");
                            Console.WriteLine();
                            Console.WriteLine("0. 나가기");
                            Console.WriteLine();
                            Console.WriteLine("원하시는 행동을 입력해주세요.");
                            Console.WriteLine();
                            Console.Write(">>");
                            string insert10 = Console.ReadLine();
                            if (insert10 == "0")
                            {

                                Console.Clear();
                                GameStart();


                                break;



                            }
                            else if (insert10 == "1")
                            {




                                continue;




                            }




















                        }




                    }









                }






























                break;

            case "6":    //상점

                Console.WriteLine("상점\r\n필요한 아이템을 얻을 수 있는 상점입니다.");
                Console.WriteLine();
                Console.WriteLine("[보유 골드]");
                Console.WriteLine($"{character.gold} G");
                Console.WriteLine();
                Console.WriteLine("[아이템 목록]");

                shop.ShowShop(false,false);
                Console.WriteLine();
                Console.WriteLine("1.아이템 구매");
                Console.WriteLine("2.아이템 구매");
                Console.WriteLine("0. 나가기");
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">>");
                string input = Console.ReadLine();
                if (input == "0")
                {


                    Console.Clear();
                    GameStart();


                    break;


                }
                else if (input == "1")
                {
                    Console.WriteLine("상점\r\n필요한 아이템을 얻을 수 있는 상점입니다.");
                    Console.WriteLine();
                    Console.WriteLine("[보유 골드]");
                    Console.WriteLine($"{character.gold} G");
                    Console.WriteLine();
                    Console.WriteLine("[아이템 목록]");
                    shop.ShowShop(true,true);
                    Console.WriteLine();
                    Console.WriteLine("0. 나가기");
                    Console.WriteLine();
                    Console.WriteLine("원하시는 행동을 입력해주세요.");
                    Console.Write(">>");
                    string input2 = Console.ReadLine();


                }
                else if (input == "2")
                {












                }













                    break;



            default:









                break;





        }






    }



}



GameStart();























void ShowStatus()
{
    Console.WriteLine("상태보기\n캐릭터의 정보가 표시됩니다.\n");
    character.CallStatus();
    Console.WriteLine();
    Console.WriteLine("0. 나가기");
    Console.WriteLine();
    Console.WriteLine("원하시는 행동을 입력해주세요.");
    Console.Write(">>");
    string insert = Console.ReadLine();
    if (insert == "0")
    {
        Console.Clear();
        GameStart();
    }

}
   
public class Character
{
    public int level = 1;
    public string name = "Chad";
    public string job = "전사";
    public int Attack = 30;
    public int Defense = 50;
    public int hp = 100;
    public int gold = 300;
    public int stamina = 20;
    public int ex = 0;

    public Inventory Inventory = new Inventory();

    public int addAttack = 0;
    public int addDefence = 0;


    public Character()
    {

        
       
        


    }

    public void CallStatus()
    {

        Console.WriteLine($" Lv.{level} \r\n{name} ( {job} )\r\n공격력 : {Attack} + ({addAttack}) \r\n방어력 : {Defense} + ({addDefence}) \r\n체 력 :{hp}\r\nGold : {gold} G\r\nStamina : {stamina}\r\n경험치 : {ex}");



    }






}


public class Inventory
{

    public List<Item> items = new List<Item>()
    {

        new Item("무쇠갑옷","방어력 +9","무쇠로 만들어져 튼튼한 갑옷입니다." ),new Item("낡은 검", "공격력 +2", "쉽게 볼 수 있는 낡은 검입니다."),
        new Item("연습용 창", "공격력 +3", "검보다는 그래도 창이 다루기 쉽죠."),new Item("고블린갑옷", "방어력 +10", "고블린 들이 제작한 갑옷입니다.")

    };

    private Item equippedWeapon = null; // 현재 장착된 무기
    private Item equippedArmor = null; // 현재 장착된 방어구


    public void ShowInventory(bool showIndex = true, bool showMark = true)
    {


        var view = items
        .OrderByDescending(it => it.Name.Length) // 길이 긴 순
        .ThenBy(it => it.Name)                   // 길이 같으면 이름순
        .ToList();


        for (int i = 0; i < items.Count; i++)
        {

            var it = view[i]; ; // <- 여기서 말한 부분


            string indexText = showIndex ? $"{i + 1}." : "";
            string mark = showMark && it. Equipped ? $" [E]." : "   ";
            Console.WriteLine($"- {indexText} {mark} {it.Name,-10}| {it.Stat,-10} | {it.Description,-30} | {it.Price} G " );
        }





    }

    public void HandleInventoryInput(string input)
    {
        if (!int.TryParse(input, out int num)) { Console.WriteLine("숫자를 입력하세요."); return; }
        if (num == 0) return;                 // 나가기
        EquipItem(num - 1);                   // 핵심: 인덱스로 변환해 공통 로직 호출
    }

    public void EquipItem(int index)
    {
        if (index < 0 || index >= items.Count)
        {
            Console.WriteLine("잘못된 번호입니다.");
            return;
        }

        Item selected = items[index];
        bool isWeapon = selected.Stat.Contains("공격력");
        bool isArmor = selected.Stat.Contains("방어력");

        // 같은 타입의 기존 장비 해제
        if (isWeapon && equippedWeapon != null)
            equippedWeapon.Equipped = false;
        if (isArmor && equippedArmor != null)
            equippedArmor.Equipped = false;

        // 새 장착
        selected.Equipped = true;
        if (isWeapon) equippedWeapon = selected;
        if (isArmor) equippedArmor = selected;

        Console.WriteLine($"{selected.Name}을(를) 장착했습니다!");
    }
}






public class Shop
{



    public List<Item> items = new List<Item>()
    {
        new Item("수련자 갑옷", "방어력 +5", "수련에 도움을 주는 갑옷입니다.", 1000),
        new Item("무쇠갑옷", "방어력 +9", "무쇠로 만들어져 튼튼한 갑옷입니다.", 2000),
        new Item("스파르타의 갑옷", "방어력 +15","스파르타의 전사들이 사용했다는 전설의 갑옷입니다.",3500),
        new Item("낡은 검","공격력 +2","쉽게 볼 수 있는 낡은 검 입니다.",600),
        new Item("청동 도끼", "공격력 +5","어디선가 사용됐던거 같은 도끼입니다.",1500),
        new Item("스파르타의 창", "공격력 +7","스파르타의 전사들이 사용했다는 전설의 창입니다.",2500)
    };

    public void ShowShop(bool showIndex = true, bool showMark = true)
    {
        for (int i = 0; i < items.Count; i++)
        {
            Item it = items[i]; // <- 여기서 말한 부분

            string indexText = showIndex ? $"{i + 1}." : "";
            string mark = showMark && it.Equipped ? $" [E]." : "   ";
            string purchased = it.Purchased ? "구매완료 " : $"{it.Price,-10} G";

            
            Console.WriteLine($"- {indexText}{mark}{it.Name, -10}| {it.Stat, -10} | {it.Description,-30} | {purchased} ")  ;
        }
    }

  



}


public class Item : IEquatable<Item>
{
    public string Name;
    public string Stat;
    public string Description;
    public int Price; // <-- 가격을 int로!
    public bool Equipped;
    public bool Purchased;

    public bool Equals(Item other)
    {
        if (other == null)
            return false;

        // 이름이 같으면 같은 아이템이라고 판단 (또는 Code, Price 등 원하는 기준 사용)
        return Name == other.Name && Stat == other.Stat && Description == other.Description;
    }


    public Item(string name, string stat, string desc, int price = 0)
    {
        Name = name;
        Stat = stat;
        Description = desc;
        Price = price;
        Equipped = false;
        Purchased = false;



    }

    public int GetAttackPower()
    {
        if (Stat.Contains("공격력"))
        {
            string number = new string(Stat.Where(char.IsDigit).ToArray());
            return int.Parse(number);
        }
        return 0; // <-- 공격력이 없을 때(방어구일 때) 0으로 처리
    }



}







public class ItemStatus
{

    private int item = 0;

    public int Item
    {
        get { return item;  }
        set { item = value; }

    }
    



}








