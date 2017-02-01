using System;

public interface IDie
{
    void Die();

    Action HasDiedAction { get; set; }
}
