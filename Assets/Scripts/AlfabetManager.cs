using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AlphabetManager : MonoBehaviour
{
    [Header("Drag di Inspector")]
    public GameObject letterButtonPrefab;
    public Transform buttonParent;
    public TextMeshProUGUI letterText;
    public Button prevButton;  // ← Drag PrevButton
    public Button nextButton;  // ← Drag NextButton
    
    private int currentLetterIndex = 0;
    private char[] alphabet = {'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z'};
    
    void Start()
    {
        if (letterButtonPrefab == null || buttonParent == null || letterText == null || prevButton == null || nextButton == null)
        {
            Debug.LogError("Drag SEMUA field!");
            return;
        }
        
        GenerateAlphabetButtons();
        UpdateLetterDisplay();
        
        // Setup Prev/Next listener
        prevButton.onClick.AddListener(PrevLetter);
        nextButton.onClick.AddListener(NextLetter);
    }
    
    void GenerateAlphabetButtons()
    {
        for (int i = 0; i < alphabet.Length; i++)
        {
            char c = alphabet[i];
            GameObject btn = Instantiate(letterButtonPrefab, buttonParent);
            TextMeshProUGUI label = btn.GetComponentInChildren<TextMeshProUGUI>();
            label.text = c.ToString();
            
            int index = i; // Capture index
            Button buttonComp = btn.GetComponent<Button>();
            buttonComp.onClick.AddListener(() => {
                currentLetterIndex = index;
                UpdateLetterDisplay();
            });
        }
    }
    
    void UpdateLetterDisplay()
    {
        letterText.text = alphabet[currentLetterIndex].ToString();
        Debug.Log("Sekarang: " + letterText.text);
    }
    
    void PrevLetter()
    {
        if (currentLetterIndex > 0) currentLetterIndex--;
        UpdateLetterDisplay();
    }
    
    void NextLetter()
    {
        if (currentLetterIndex < alphabet.Length - 1) currentLetterIndex++;
        UpdateLetterDisplay();
    }
}
