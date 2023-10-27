# ICIGame

## Class Game() : PersistantSingleton<Game\>

 BattleScene에서 사용하는시작용 클레스
 Start() 
 - 이 내부의 내용을 실행시작한다.

## Class StageSetting : PersistantSingleton<StageSetting\>

  BattleScene의 필드를 설정하기위한 클래스
  buildWall()
  - stage값에따라 wall을 생성함

  buildEnemy()
  - stage값에따라 Enemy를 생성함

<br><br>

# Character

## Class PlayerData

생성한 플레이어를 저장하는 용도

## Class Character(Pos pos, int hp, int speed, int level) : LifeBaseObject, TurnObserver, IApplyPos<Character\>

한 캐릭터에 관련한 모든정보를 저장하는 클레스
  void Skill1(), Skill2(), Skill3()
  - 캐릭터가 할수있는 공격행동

## Class CharacterMap : Singleton<CharacterMap\>

캐릭터를 저장하고 캐릭터의 위치를 추적하기위한용도 위치에 관한계산을 함

## Class Charactermove : Singleton<=>

캐릭터의 정보를 가지 움직임 명령을 처리함 + 움직임에서 발생하는 예외상황 처리

<br><br>

# Camera

## Class CameraMove : MonoBehavior

마우스의 위치를 이용해 카매라의 xz 움직임을 만듦

## Class CameraStacking : MonoBehavior

자기 자신의 카메라는 stack해줌 여러개의 카매라를 동시에 겹처서쓰기위햇 필요함

# Enemy

## Class Enemy(Pos pos, int hp, int speed) : LifeBaseObject, TurnObserver, IApplyPos<Enemy\>
적을 표현하는 데이터

## Class EnemeysMap: Singleton<EnemysMap\>
적 데이터를 추가&제거 하고 위치와 숫자를 확인하는 함수로 이루어져 있다.

## Class AITrace(Pos objectPos, int detect LimitSize, int distanceToPurpose)
추적하는 AI
- Pos Action()
direction을 계산해서 목표에 도달했는지 확인하고 chooseDirection메서드를 통해서 위치를 방향(direction) 을 반환해줌

## Checker(Pos pos, int LimitStep)
체커를 처음 생성할때 쓰는 생성자 

## Checker(Pos pos, Checker nearChecker)
체커가 체커를 생성할때 쓰는 생성자

- spread(Queue<Checker\> checkers)
체커를 움직일수있는4방향으로 확인후 생성

- absorption()
체커 병합 같은 pos에 2개 이상의 체커가 존제하면 두정보를 병합시킨다.

- setSizeOfNearCheckerDistance()
근처의체커 거리(distance) 변수를 재설정해줌

- getFirstStepDirection(Checker purpose, int distanceTop)
처음 방향을 역추적해서 구하는 메서드

## RangeChecker(int limitStep, int distanceToPurpose)
범위를 추적하기위해 확인하는 클래스

- Action(Queue<Checker\> checkers)


<br><br><br>

# Internal System Class

## Class Singleton

Instance라는 객체를 활용하여 게임내에 하나의 인스턴스만 존제하는 클레스를 만든다.

## Class Persistant Singleton

Monobehavior 를 활용하기위한 Singleton이다. 씬이 바뀌어도 사라지지 않게한다.

## Class PopupManager : Singleton<PopupManager\>

void SetBeacon(GameObject beacon)
- 팝업에 부모가될 비콘을 세팅하는 메소드

static PopupBase CreatePopup(string name)
- 리소스를 가지고와 팦업을 생성하고 새팅해주는 매소드

static T OpenPopup >T>(string popupName) where T: PopupBase
- 파라미터의 팦업이 존제하는지 확인하고 존제하지않는경우생성하고, 존재한경우 Active시킴

static bool ClosePopup(PopupBase T)
- 해당팝업을 unActive시킴

## Class ResoucesManager : Singleton<ResoucesManager\>

GameObject GetResouces(eResoucesPath, string name)(string path)
- 리소스가저오거

string getPath(eResoucesPath resoucesPath)
- path를 단축시켜 쉽게쓸수있게만듦
