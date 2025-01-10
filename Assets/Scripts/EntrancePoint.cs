using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntrancePoint : MonoBehaviour {
    public static EntrancePoint instance;
    private void Awake() {
        instance = this;
    }
    
    //DODAJ ZEBY SIE PODMIENIAL NA AKTUALNY PRZY WEJSCIU DO POMIESZCZENIA JEZELI BEDZIEMY CHCIELI MOZLIWOSC POWROTU DO POMIESZCZEN
}
