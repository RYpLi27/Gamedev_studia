using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitPoint : MonoBehaviour
{
    public static ExitPoint instance;
    private void Awake() {
        instance = this;
    }
    
    //DODAJ ZEBY SIE PODMIENIAL NA AKTUALNY PRZY WEJSCIU DO POMIESZCZENIA JEZELI BEDZIEMY CHCIELI MOZLIWOSC POWROTU DO POMIESZCZEN
}