using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProcessMemoryReaderLib;
using System.Globalization;
using System.Diagnostics;


namespace bischExe
{
    public partial class bischExe : Form
    {
        private Process[] MyProcess;
        private ProcessModule mainModule;
        private ProcessMemoryReader Mem = new ProcessMemoryReader();
        private PlayerData MainPlayer = new PlayerData();

        private PlayerDataAddr MainPlayerOffsets = new PlayerDataAddr();
        private List<PlayerData> EnemyAddresses = new List<PlayerData>();

        private float PI = 3.14159265F;

        private bool GameFound = false;
        private bool FocusingOnEnemy = false;
        private int FocusTarget = -1;

        #region TESTING MOUSE X AND MOUSE Y(THIS CAN BE IGNORED)

        private void upBTN_Click(object sender, EventArgs e)
        {
            Mem.WriteFloat(MainPlayer.baseAddress + MainPlayer.offsets.pitch, Mem.ReadFloat(MainPlayer.baseAddress + MainPlayer.offsets.pitch) + 10.0f);
        }

        private void rightBTN_Click(object sender, EventArgs e)
        {
            Mem.WriteFloat(MainPlayer.baseAddress + MainPlayer.offsets.yaw, Mem.ReadFloat(MainPlayer.baseAddress + MainPlayer.offsets.yaw) + 10.0f);
        }

        private void leftBTN_Click(object sender, EventArgs e)
        {
            Mem.WriteFloat(MainPlayer.baseAddress + MainPlayer.offsets.yaw, Mem.ReadFloat(MainPlayer.baseAddress + MainPlayer.offsets.yaw) - 10.0f);
        }

        private void downBTN_Click(object sender, EventArgs e)
        {
            Mem.WriteFloat(MainPlayer.baseAddress + MainPlayer.offsets.pitch, Mem.ReadFloat(MainPlayer.baseAddress + MainPlayer.offsets.pitch) - 10.0f);
        }

        #endregion TESTING MOUSE X AND MOUSE Y(THIS CAN BE IGNORED)

        public bischExe()
        {
            InitializeComponent();
        }



        private void GameChoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < MyProcess.Length; i++)
                {
                    if (GameChoice.SelectedItem.ToString().Contains(MyProcess[i].ProcessName))
                    {
                        MyProcess[0] = Process.GetProcessById(int.Parse(GameChoice.Text.Replace(MyProcess[i].ProcessName + "-", "")));
                        mainModule = MyProcess[0].MainModule;
                        Mem.ReadProcess = MyProcess[0];
                        Mem.OpenProcess();
                        GameFound = true;

                    
                        MainPlayer.baseAddress = Mem.ReadInt(0x50f4f4);
                        MainPlayer.offsets = new PlayerDataAddr();
                        SetupEnemyVars();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kunne ikke finde klienten, prøv igen: \n " + ex.Message, "Error");
            }
        }

        private void GameChoice_Click(object sender, MouseEventArgs e)
        {
            GameChoice.Items.Clear();
            MyProcess = Process.GetProcesses();
            for (int i = 0; i < MyProcess.Length; i++)
            {
                GameChoice.Items.Add(MyProcess[i].ProcessName + "-" + MyProcess[i].Id);
            }
        }

        private void CustomAimbot_Load(object sender, EventArgs e)
        {
            ProcessTMR.Enabled = true;
        }

        private void ProcessTMR_Tick(object sender, EventArgs e)
        {
            if (GameFound)
            {
                xMouse.Text = "xMouse: " + Mem.ReadFloat(MainPlayer.baseAddress + MainPlayer.offsets.yaw).ToString();
                yMouse.Text = "yMouse: " + Mem.ReadFloat(MainPlayer.baseAddress + MainPlayer.offsets.pitch).ToString();
           
                xPosLabel.Text = "xPos: " + Mem.ReadFloat(MainPlayer.baseAddress + MainPlayer.offsets.xPos).ToString();
                yPosLabel.Text = "yPos: " + Mem.ReadFloat(MainPlayer.baseAddress + MainPlayer.offsets.yPos).ToString();
                zPosLabel.Text = "zPos: " + Mem.ReadFloat(MainPlayer.baseAddress + MainPlayer.offsets.zPos).ToString();

              

                playerHealth.Text = "Health: " + Mem.ReadInt(MainPlayer.baseAddress + MainPlayer.offsets.health).ToString();


                //RIGHT MOUSE
                int res = ProcessMemoryReaderApi.GetKeyState(02);
                if ((res & 0x8000) != 0)
                {
       
                    FocusingOnEnemy = true;
                    AimBot();
                }
                else
                {
                   
                    FocusingOnEnemy = false;
                    FocusTarget = -1;
                }
            }

            try
            {
                if (MyProcess != null)
                {
                    if (MyProcess[0].HasExited)
                    {
                        GameFound = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Der opstod en fejl, prøv igen:\n " + ex.Message);
            }
        }

        private void AimBot()
        {
            //Grab our player's information
            PlayerDataVec playerDataVec = GetPlayerVecData(MainPlayer);
            List<PlayerDataVec> enemiesDataVec = new List<PlayerDataVec>();

            for (int i = 0; i < EnemyAddresses.Count; i++)
            {
                
                PlayerDataVec enemyDataVector = GetPlayerVecData(EnemyAddresses[i]);
                
                enemiesDataVec.Add(enemyDataVector);
            }

            
            if (playerDataVec.health > 0)
            {
                int target = 0;
                if (FocusingOnEnemy && FocusTarget != -1)
                {
                    
                    if (enemiesDataVec[FocusTarget].health > 0)
                        target = FocusTarget;
                    else target = FindClosestEnemyIndex(enemiesDataVec.ToArray(), playerDataVec);
                }
                else
                    target = FindClosestEnemyIndex(enemiesDataVec.ToArray(), playerDataVec);

                
                if (target != -1) //-1 means something went wrong
                {
                    FocusTarget = target;
                    
                    
                    if (enemiesDataVec[target].health > 0)
                        AimAtTarget(enemiesDataVec[target], playerDataVec);
                }
            }
        }

        

        private int FindClosestEnemyIndex(PlayerDataVec[] enemiesDataVec, PlayerDataVec myPosition)
        {
            float[] distances = new float[enemiesDataVec.Length];

            
            for (int i = 0; i < enemiesDataVec.Length; i++)
            {
                
                if (enemiesDataVec[i].health > 0)
                    distances[i] = Get3dDistance(enemiesDataVec[i], myPosition);
                else
                    
                    distances[i] = float.MaxValue;
            }
            
            float[] newDistances = new float[distances.Length];
            Array.Copy(distances, newDistances, distances.Length);

            
            Array.Sort(newDistances);

            
            for (int i = 0; i < distances.Length; i++)
            {
                if (distances[i] == newDistances[0])
                {
                    return i;
                }
            }
            return -1;
        }

        private float Get3dDistance(PlayerDataVec to, PlayerDataVec from)
        {
            return (float)
            (Math.Sqrt(
            ((to.xPos - from.xPos) * (to.xPos - from.xPos)) +
            ((to.yPos - from.yPos) * (to.yPos - from.yPos)) +
            ((to.zPos - from.zPos) * (to.zPos - from.zPos))
            ));
        }

        private void AimAtTarget(PlayerDataVec enemyDataVector, PlayerDataVec playerDataVector)
        {
            float yaw = -(float)Math.Atan2(enemyDataVector.xPos - playerDataVector.xPos, enemyDataVector.yPos - playerDataVector.yPos)
                            / PI * 180 + 180;

            float pitch = (float)Math.Asin((enemyDataVector.zPos - playerDataVector.zPos) /
                            Get3dDistance(enemyDataVector, playerDataVector))
                         * 180 / PI;

            
            Mem.WriteFloat(MainPlayer.baseAddress + MainPlayer.offsets.yaw, yaw);
            Mem.WriteFloat(MainPlayer.baseAddress + MainPlayer.offsets.pitch, pitch);
        }

        
        private PlayerDataVec GetPlayerVecData(PlayerData updatePlayer)
        {
            PlayerDataVec playerRet = new PlayerDataVec();

            playerRet.yaw = Mem.ReadFloat(updatePlayer.baseAddress + updatePlayer.offsets.yaw);
            playerRet.pitch = Mem.ReadFloat(updatePlayer.baseAddress + updatePlayer.offsets.pitch);

            playerRet.xPos = Mem.ReadFloat(updatePlayer.baseAddress + updatePlayer.offsets.xPos);
            playerRet.yPos = Mem.ReadFloat(updatePlayer.baseAddress + updatePlayer.offsets.yPos);
            playerRet.zPos = Mem.ReadFloat(updatePlayer.baseAddress + updatePlayer.offsets.zPos);

            playerRet.health = Mem.ReadInt(updatePlayer.baseAddress + updatePlayer.offsets.health);
            return playerRet;
        }

       
        private void SetupEnemyVars()
        {
            PlayerData En1 = new PlayerData();
            //SETUP ENEMY VARIABLES
            En1.baseAddress = Mem.ReadInt(Mem.ReadInt(0x50f4f8) + 0x4);
            En1.offsets = MainPlayer.offsets;
            EnemyAddresses.Add(En1);

            PlayerData En2 = new PlayerData();
            En2.baseAddress = Mem.ReadInt(Mem.ReadInt(0x50f4f8) + 0x8);
            En2.offsets = MainPlayer.offsets;
            EnemyAddresses.Add(En2);

            PlayerData En3 = new PlayerData();
            En3.baseAddress = Mem.ReadInt(Mem.ReadInt(0x50f4f8) + 0xc);
            En3.offsets = MainPlayer.offsets;
            EnemyAddresses.Add(En3);

            PlayerData En4 = new PlayerData();
            En4.baseAddress = Mem.ReadInt(Mem.ReadInt(0x50f4f8) + 0x10);
            En4.offsets = MainPlayer.offsets;
            EnemyAddresses.Add(En4);

            PlayerData En5 = new PlayerData();
            En5.baseAddress = Mem.ReadInt(Mem.ReadInt(0x50f4f8) + 0x14);
            En5.offsets = MainPlayer.offsets;
            EnemyAddresses.Add(En5);

            PlayerData En6 = new PlayerData();
            En6.baseAddress = Mem.ReadInt(Mem.ReadInt(0x50f4f8) + 0x18);
            En6.offsets = MainPlayer.offsets;
            EnemyAddresses.Add(En6);

            PlayerData En7 = new PlayerData();
            En7.baseAddress = Mem.ReadInt(Mem.ReadInt(0x50f4f8) + 0x1C);
            En7.offsets = MainPlayer.offsets;
            EnemyAddresses.Add(En7);

            PlayerData En8 = new PlayerData();
            En8.baseAddress = Mem.ReadInt(Mem.ReadInt(0x50f4f8) + 0x20);
            En8.offsets = MainPlayer.offsets;
            EnemyAddresses.Add(En8);

            PlayerData En9 = new PlayerData();
            En9.baseAddress = Mem.ReadInt(Mem.ReadInt(0x50f4f8) + 0x24);
            En9.offsets = MainPlayer.offsets;
            EnemyAddresses.Add(En9);

            PlayerData En10 = new PlayerData();
            En10.baseAddress = Mem.ReadInt(Mem.ReadInt(0x50f4f8) + 0x28);
            En10.offsets = MainPlayer.offsets;
            EnemyAddresses.Add(En10);

            PlayerData En11 = new PlayerData();
            En11.baseAddress = Mem.ReadInt(Mem.ReadInt(0x50f4f8) + 0x2C);
            En11.offsets = MainPlayer.offsets;
            EnemyAddresses.Add(En11);

            PlayerData En12 = new PlayerData();
            En12.baseAddress = Mem.ReadInt(Mem.ReadInt(0x50f4f8) + 0x30);
            En12.offsets = MainPlayer.offsets;
            EnemyAddresses.Add(En12);

            PlayerData En13 = new PlayerData();
            En13.baseAddress = Mem.ReadInt(Mem.ReadInt(0x50f4f8) + 0x34);
            En13.offsets = MainPlayer.offsets;
            EnemyAddresses.Add(En13);

            PlayerData En14 = new PlayerData();
            En14.baseAddress = Mem.ReadInt(Mem.ReadInt(0x50f4f8) + 0x38);
            En14.offsets = MainPlayer.offsets;
            EnemyAddresses.Add(En14);

            PlayerData En15 = new PlayerData();
            En15.baseAddress = Mem.ReadInt(Mem.ReadInt(0x50f4f8) + 0x3C);
            En15.offsets = MainPlayer.offsets;
            EnemyAddresses.Add(En15);

            PlayerData En16 = new PlayerData();
            En16.baseAddress = Mem.ReadInt(Mem.ReadInt(0x50f4f8) + 0x40);
            En16.offsets = MainPlayer.offsets;
            EnemyAddresses.Add(En16);
        }

        private void XPosLabel_Click(object sender, EventArgs e)
        {

        }

        private void XMouse_Click(object sender, EventArgs e)
        {

        }
    }
}
