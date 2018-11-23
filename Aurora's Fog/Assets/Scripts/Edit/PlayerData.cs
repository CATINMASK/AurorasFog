using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour {

    FPController fpcontrollerST;

    // Attributes
    public byte strength = 1;
    public byte endurance = 1;
    public byte agility = 1;
    public byte perception = 1;
    public byte intelligence = 1;
    public byte charisma = 1;

    public byte points = 12;

    // Player Stats:
    public float maxWeight = 10;

    public float health = 60;
    float maxHealth = 60;

    public float stamina = 60;
    float maxStamina = 60;

    public float mana = 10;
    float maxMana = 10;

    public bool fatty;
    public bool liar;
    public bool gifted;
    public string playerName;

    public void Awake()
    {
        fpcontrollerST = gameObject.GetComponent<FPController>();
        SetAllStats();
    }

    void Update()
    {
        //CheckStamina();
        //if (fpcontrollerST.running && fpcontrollerST.canRun)
        //    Running();
    }

    public void Running()
    {
        stamina -= Time.deltaTime * 5;  // 12 seconds
    }

    void CheckStamina()
    {
        if (stamina >= maxStamina)
            stamina = maxStamina;
        //if (stamina < maxStamina && !fpcontrollerST.running)
        //    stamina += Time.deltaTime / 2;
        /*
        if (stamina <= 0.0f)
        {
            stamina = 0;
            fpcontrollerST.OffRunning();
            fpcontrollerST.canRun = false;
            fpcontrollerST.running = false;
        }
        if (stamina >= stamina / 2)
            fpcontrollerST.canRun = true;
        */
    }

    public void SetAllStats()
    {
        SetMaxHealth();
        SetMaxMana();
        SetMaxStamina();
        SetMaxWeight();
        SetPlayerSpeed();
    }

    public void SetMaxWeight()
    {
        maxWeight = strength * 10;
    }
    public void SetMaxHealth()
    {
        if (health == maxHealth)
        {
            maxHealth = 50 + endurance * 10;
            health = maxHealth;
        }
        else
            maxHealth = 50 + endurance * 10;
    }
    public void SetMaxStamina()
    {
        if (stamina == maxStamina)
        {
            maxStamina = 50 + endurance * 10;
            stamina = maxStamina;
        }
        else
            maxStamina = 50 + endurance * 10;
    }
    public void SetMaxMana()
    {
        if (mana == maxMana)
        {
            maxMana = 10 + intelligence * 10;
            mana = maxMana;
        }
        else
            maxMana = 10 + intelligence * 10;
    }
    public void SetPlayerSpeed()
    {
        //gameObject.GetComponent<FPController>().movement.RunSpeed = 5 + agility * 0.5f;
    }

    public byte GetStrength()
    {
        return strength;
    }
    public void SetStrength (byte value)
    {
        strength = value;
    }
    public void IncStrength()
    {
        if (strength < 10 && points > 0)
        {
            strength++;
            points--;
            SetAllStats();
            GameObject.Find("customization").GetComponent<Customization>().IncreaseSTR();
        }
    }
    public void DecStrength()
    {
        if (strength > 1)
        {
            strength--;
            points++;
            SetAllStats();
            GameObject.Find("customization").GetComponent<Customization>().DecreaseSTR();
        }
    }

    public byte GetEndurance()
    {
        return endurance;
    }
    public void SetEndurance(byte value)
    {
        endurance = value;
    }
    public void IncEndurance()
    {
        if (endurance < 10 && points > 0)
        {
            endurance++;
            points--;
            SetAllStats();
        }
    }
    public void DecEndurance()
    {
        if (endurance > 1)
        {
            endurance--;
            points++;
            SetAllStats();
        }
    }

    public byte GetAgility()
    {
        return agility;
    }
    public void SetAgility(byte value)
    {
        agility = value;
    }
    public void IncAgility()
    {
        if (agility < 10 && points > 0)
        {
            agility++;
            points--;
            SetAllStats();
        }
    }
    public void DecAgility()
    {
        if (agility > 1)
        {
            agility--;
            points++;
            SetAllStats();
        }
    }

    public byte GetPerception()
    {
        return perception;
    }
    public void SetPerception(byte value)
    {
        perception = value;
    }
    public void IncPerception()
    {
        if (perception < 10 && points > 0)
        {
            perception++;
            points--;
            SetAllStats();
        }
    }
    public void DecPerception()
    {
        if (perception > 1)
        {
            perception--;
            points++;
            SetAllStats();
        }
    }

    public byte GetIntelligence()
    {
        return intelligence;
    }
    public void SetIntelligence(byte value)
    {
        intelligence = value;
    }
    public void IncIntelligence()
    {
        if (intelligence < 10 && points > 0)
        {
            intelligence++;
            points--;
            SetAllStats();
        }
    }
    public void DecIntelligence()
    {
        if (intelligence > 1)
        {
            intelligence--;
            points++;
            SetAllStats();
        }
    }

    public byte GetCharisma()
    {
        return charisma;
    }
    public void SetCharisma(byte value)
    {
        charisma = value;
    }
    public void IncCharisma()
    {
        if (charisma < 10 && points > 0)
        {
            charisma++;
            points--;
            SetAllStats();
        }
    }
    public void DecCharisma()
    {
        if (charisma > 1)
        {
            charisma--;
            points++;
            SetAllStats();
        }
    }

}
