using UnityEngine;
using System.Collections.Generic;

public interface IValidatable
{
    void Validate(ValidationResult result);
}

public class ValidationResult
{
    public readonly List<string> Errors = new();

    public readonly List<string> Warnings = new();
}