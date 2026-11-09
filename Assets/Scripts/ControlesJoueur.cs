using UnityEngine;
using UnityEngine.InputSystem;
public enum EtatJoueur{

    LIBRE,

    MORT,

    ATTAQUE,

    SAUT,

    INTERACTION
}

public class ControlesJoueur : MonoBehaviour
{
    [SerializeField] InputAction deplacement;

    [SerializeField] InputAction saut;

    [SerializeField] InputAction dash;

    [SerializeField] EtatJoueur etatJoueur;

    Vector3 deplacementTotal;

    Vector3 deplacementGeneral;

    Vector3 deplacementVertical;

    Vector3 deplacementSpecial;

    deplacementJoueur deplacementJoueur;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void OnEnable()
    {
        deplacement.Enable();
        saut.Enable();
        dash.Enable();
    }

    void OnDisable()
    {
        deplacement.Disable();
        saut.Disable();
        dash.Disable();
    }
    void Start()
    {
       deplacementJoueur = GetComponent<deplacementJoueur>();
       etatJoueur = EtatJoueur.LIBRE;
    }

    // Update is called once per frame
    void Update()
    {
        deplacementTotal = Vector3.zero;

        Vector2 joueurInput = deplacement.ReadValue<Vector2>();
        bool sautInput = saut.WasPressedThisFrame();
        bool dashInput = dash.WasPressedThisFrame();

        if (etatJoueur == EtatJoueur.LIBRE || etatJoueur == EtatJoueur.SAUT)
        {
            deplacementGeneral = deplacementJoueur.CalculerDeplacementGeneral(joueurInput);

            if (sautInput == true)
            {
                deplacementJoueur.Sauter();
            }
            if (dashInput == true)
            {
               deplacementJoueur.Dash();
            }
        }

         deplacementVertical = deplacementJoueur.CalculerDeplacementVertical();
         deplacementSpecial = deplacementJoueur.CalculerDeplacementSpecial();

         deplacementTotal = deplacementGeneral + deplacementVertical + deplacementSpecial;

         deplacementJoueur.MettreAJourDeplacement(deplacementTotal);
         deplacementJoueur.MettreAJourRotation(joueurInput.x);
    }
}
