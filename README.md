
# UnityDOW

## Class Game() : PersistantSingleton<Game\>

 <p>BattleScene에서 사용하는시작용 클레스</p>
 <p>Start() <br>
 - 이 내부의 내용을 실행시작한다.
 </p>

## Class StageSetting : PersistantSingleton<StageSetting>

<p>  BattleScene의 필드를 설정하기위한 클래스</p>
<p>
  buildWall()<br>
  - stage값에따라 wall을 생성함
</p>
<p>
  buildEnemy()<br>
  - stage값에따라 Enemy를 생성함
</p>

<br><br>

# Character

## Class PlayerData

<p>생성한 플레이어를 저장하는 용도</p>

## Class Character(Pos pos, int hp, int speed, int level) : LifeBaseObject, TurnObserver, IApplyPos<Character>

<p>한 캐릭터에 관련한 모든정보를 저장하는 클레스</p>
<p>
  void Skill1(), Skill2(), Skill3()<br>
  - 캐릭터가 할수있는 공격행동
</p>

## Class CharacterMap : Singleton<CharacterMap>

<p>캐릭터를 저장하고 캐릭터의 위치를 추적하기위한용도 위치에 관한계산을 함 </p>

## Class Charactermove : Singleton<=>

<p>캐릭터의 정보를 가지 움직임 명령을 처리함 + 움직임에서 발생하는 예외상황 처리 </p>

<br><br>

# Camera

## Class CameraMove : MonoBehavior

<p>마우스의 위치를 이용해 카매라의 xz 움직임을 만듦</p>

## Class CameraStacking : MonoBehavior

<p>자기 자신의 카메라는 stack해줌 여러개의 카매라를 동시에 겹처서쓰기위햇 필요함 </p>

  <br><br><br>

# Internal System Class

## Class Singleton

Instance라는 객체를 활용하여 게임내에 하나의 인스턴스만 존제하는 클레스를 만든다.

## Class Persistant Singleton

Monobehavior 를 활용하기위한 Singleton이다. 씬이 바뀌어도 사라지지 않게한다.

## Class PopupManager : Singleton<PopupManager>

<p>
  void SetBeacon(GameObject beacon)
  - 팝업에 부모가될 비콘을 세팅하는 메소드
</p>
<p>
  static PopupBase CreatePopup(string name)
  - 리소스를 가지고와 팦업을 생성하고 새팅해주는 매소드
</p>
<p>
  static T OpenPopup >T>(string popupName) where T: PopupBase
  - 파라미터의 팦업이 존제하는지 확인하고 존제하지않는경우생성하고, 존재한경우 Active시킴
</p>
<p>
  static bool ClosePopup(PopupBase T)
  - 해당팝업을 unActive시킴
</p>

## Class ResoucesManager : Singleton<ResoucesManager>

<p>
  GameObject GetResouces(eResoucesPath, string name)(string path)
  - 리소스가저오거
</p>
<p>
  string getPath(eResoucesPath resoucesPath)
  - path를 단축시켜 쉽게쓸수있게만듦
</p>
