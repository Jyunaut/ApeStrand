using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class GameState : StateMachine
{
    public GameManager gameManager;

    public GameState(GameManager gm)
    {
        this.gameManager = gm;
    }

    public bool Menu()
    {
        if (gameManager.btnMenu()) return true;
        return false;
    }

    // public bool Pause()
    // {
    //     if(gameManager.btnMenu()) return true;
    //     return false;
    // }

    public bool Game()
    {
        if (gameManager.btnGame()) return true;
        return false;
    }

    public bool Win()
    {
        if (gameManager.btnWin()) return true;
        return false;
    }

    public bool Lose()
    {
        if (gameManager.btnLose()) return true;
        return false;
    }
}

class Title : GameState
{
    public Title(GameManager gm) : base(gm) { }
    public override void EnterState()
    {
        SceneManager.LoadScene(1);
        Debug.Log("Load Menu scene");
    }
}
// class Pause : GameState
// {
//     public Pause(GameManager gm) : base(gm) { }
//     // TODO: Research
// }
class Game : GameState
{
    public Game(GameManager gm) : base(gm) { }
    public override void EnterState()
    {
        SceneManager.LoadScene(2);
        Debug.Log("Load Game scene");
    }
}
class Win : GameState
{
    public Win(GameManager gm) : base(gm) { }
    public override void EnterState()
    {
        SceneManager.LoadScene(4);
        Debug.Log("Load Win scene");
    }
}
class Lose : GameState
{
    public Lose(GameManager gm) : base(gm) { }
    public override void EnterState()
    {
        SceneManager.LoadScene(3);
        Debug.Log("Load Lose scene");
    }
}