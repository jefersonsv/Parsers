# Documentos
Parse, validate, generate, bind many types of documents

# Analisar documento CPF

```c#
CPF.IsValid("562.832.510-48") // returns true;
CPF.IsValid("56283251048") // returns true;
CPF.IsValid("562.832.510-00") // returns false;
CPF.IsValid("56283251000") // returns false;
```

