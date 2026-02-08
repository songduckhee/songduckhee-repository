# songduckhee-repository
<img src="https://capsule-render.vercel.app/api?type=Rounded&height=200&color=gradient&customColorList=4&text=Songduckhee&fontColor=FFF8DB" />

# 자기소개

안녕하세요 클라이언트 개발자를 목표로 하고 있는 송덕희 입니다. c#과 유니티를 공부하여 오랫동안 개발자로 일하고 싶은 꿈을 가지고 있습니다.

# 개인 프로젝트


## 🏧ATM System

<img width="871" height="490" alt="Image" src="https://github.com/user-attachments/assets/33a1a0c2-05df-4a7b-ba24-50d22fe7648b" />


📌 **프로젝트 소개**

팀 스파르타에서 진행하는 유니티 12기 게임개발 심화 개인과제

⏱ **개발 기간**  

2025/11/24 ~ 2025/11/28

**코드 구조**

- 명확한 아키텍처 설계를 먼저 하지는 않았지만,
- SOLID 원칙을 기준으로 기존 코드 구조를 점검하고
- 각 스크립트가 하나의 책임을 갖도록 기능 단위로 분리하여 리팩토링했습니다.

**주요 기능**
* 로그인 기능
* 통장 잔액을 다른 유저에게 입금, 출금할 수 있는 기능

## 📚️ 기술스택

### ✔️ Language
c#
### ✔️ Version Control
Git / GitHub
### ✔️ IDE
Visual Studio
### ✔️ Framework
<img src = "https://github.com/user-attachments/assets/16e6d688-670d-46fd-abc4-88f8b4ccb637" width="3%" height="height size%"> (2022.3.62f2)

## Trouble Shooting
<img width="870" height="492" alt="Image" src="https://github.com/user-attachments/assets/64f7fb2e-a4f1-46e7-b920-590e881dfe22" />

- 버튼 배열을 순회하며 addListener를 등록하는 과정에서
  for문 외부에 선언된 인덱스 변수를 사용해
  모든 버튼이 마지막 인덱스를 참조하는 문제가 발생함
- 인덱스 변수의 선언과 할당을 for문 내부에서 수행하도록 수정하여 해결
```
for (int i = 0; i < button.Length; i++)
{
	int q = i;
	button[i].onClick.AddListener(() => Deposit(q));
}
```

##


