using ICI;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace ICI
{
    public class Pos
    {
        public int x, z;
        public readonly int valuea;

        public Pos(int x, int z)
        {
            this.x = x;
            this.z = z;
        }
        public Pos(Pos pos)
        {
            this.x = pos.x;
            this.z = pos.z;
        }
        public Pos(Vector3 pos)
        {
            this.x = (int)pos.x;
            this.z = (int)pos.z;
        }

        public static Pos Zero() => new Pos(0, 0);
        
        public static Pos Right() => new Pos(1, 0);
        public static Pos Left() => new Pos(-1, 0);
        public static Pos Front() => new Pos(0, 1);
        public static Pos Back() => new Pos(0, -1);

        public static Pos FrontRight() => new Pos(1, 1);
        public static Pos FrontLeft() => new Pos(-1, 1);
        public static Pos BackRight() => new Pos(1, -1);
        public static Pos BackLeft() => new Pos(-1, -1);

        public static Vector3 Pos2Vector(Pos pos)
        {
            return new Vector3(pos.x, 0, pos.z);
        }

        public static Pos operator -(Pos A)
        {
            return new Pos(-A.x, -A.z);
        }

        public static Pos operator +(Pos A, Pos B)
        {
            return new Pos(A.x + B.x, A.z + B.z);
        }

        public static Pos operator -(Pos A, Pos B)
        {
            return new Pos(A.x - B.x, A.z - B.z);
        }

        public static Pos operator *(Pos A, int B)
        {
            return new Pos(A.x * B, A.z * B);
        }
        public static Pos operator *(int A, Pos B)
        {
            return new Pos(A * B.x, A * B.z);
        }
        public static List<Pos> Range(Pos startingPoint, Pos direction, int range)
        {
            List<Pos> result = new List<Pos>();
            for (int i = 0; i < range; i++)
            {
                result.Add(startingPoint + (direction * i));
            }
            return result;
        }

        public override bool Equals (object other)
        {
            if (other == null) return false;
            if (other is not Pos) return false;

            return (this.x == ((Pos)other).x && this.z == ((Pos)other).z);
        }

        public override int GetHashCode()
        {
            return this.x.GetHashCode() ^ this.z.GetHashCode();
        }

        public Pos Clone()
        {
            return new Pos(this.x, this.z);
        }

        public void addPos(int x, int z)
        {
            this.x+=x; this.z+=z;
        }

        public override string ToString()
        {
            return $"({this.x}, {this.z})";
        }

    }

    public class GamePlay : Singleton<GamePlay>
    {
        public bool isGamePlaying;


        // 플레이어 캐릭터 배치
        private void setPlayerCharacterPos(List<Pos> characterPos)
        {
            for (int i = 0; i < characterPos.Count; i++)
            {
                PlayerData.PlayerCharacters[i].pos = characterPos[i];
            }
        }


        public override void Initialize()
        {
        }
    }
}
