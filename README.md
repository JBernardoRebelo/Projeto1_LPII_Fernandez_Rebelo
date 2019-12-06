# 1º Projeto de Linguagens de Programação II 2019/2020 - *IMDB Database Search*

### Autores

*[João Rebelo - a21805230](https://github.com/JBernardoRebelo)*

*[Miguel Fernández - a21803644](https://github.com/MizuRyujin)*

### Repositório Git

[Projeto 1 LPII, Fernandez e Rebelo](https://github.com/JBernardoRebelo/Projeto1_LPII_Fernandez_Rebelo)

### Quem fez o quê

**João**: Ficheiro *README*. Leitura e conversão de ficheiros para coleções. Ordenação de coleções. `class ITitle` e derivadas, `class TitleLoader`.

**Miguel**: UML, `class SearchLoop`, menus, garantir que programa leia independentemente da linguagem do computador.

## Descrição e arquitetura da Solução

O programa foi organizado com lógicas parecidas a projetos anteriores,
definimos que a classe `program` só teria a inicialização do programa geral,
chamando o método `Loop()` através de uma instância da `class SearchLoop`.

Esta classe é responsável pelo ciclo de pesquisa gerindo o _input_ do
utilizador. Criámos uma interface `ITitle` que contém um `ID` que é comum a
todos os _Titles_, com isto fizémos com que as `structs` `TitleBasic` e
`TitleRating` herdassem desta.

A `struct TitleBasic` contem os parâmetros
necessários para instanciar os _Titles_ vindos do ficheiro
`title.basics.tsv.gz`, enquanto que a `struct TitleRating` tem os parâmetros
para instanciar os _Titles_ vindos do ficheiro `title.ratings.tsv.gz`.

A classe `TitleLoader` é responsável por converter os ficheiros dados em
coleções de `TitleRating` e `TitleBasic` que são inicializadas na classe
`SearchLoop` onde, como referido anteriormente, é feito o ciclo de pesquisa.

A `class Render` é usada para demonstrar e escrever na consola o pretendido
pelo utilizador e informações de menus.

A `class FileHandler` contém métodos para descomprimir e converter ficheiros
de modo a que o programa consiga ler e fazer as conversões necessárias
posteriormente.

Tentámos ao máximo utilizar coleções no seu nível mais abstrato
`IEnumerable` ou `ICollection`, no entanto existem métodos que só terão o
comportamento pretendido na utilização de `List`. As coleções de `ITitle` e
dos seus filhos são geralmente `ICollection` apenas em casos específicos em
que são `List` para se poder aceder a métodos mais específicos. A
`class TitleBasic` tem uma propriedade `Genres` para guardar os géneros de
cada _Title_ que é um `Hashset<string>` de forma a preparar para a pesquisa
por géneros.

## Diagrama UML

## Referências

### Webgrafia

#### *Slides* de Aula do professor Nuno Fachada:

- **Aula 6**, I/O _streams_ e ficheiros.
- **Aula 7**,  _Delegates_, Eventos.
- **Aula 8**, Expressões Lambda, Tratamento de _nulls_, Expressões de pesquisa e LINQ
- **Aula 9**, *Design Patterns* e princípios gerais de POO

#### Markdown

- *[Markdown Cheatsheet](https://github.com/adam-p/markdown-here/wiki/Markdown-Cheatsheet)*
- *[Markdown Guide](https://www.markdownguide.org/)*

#### *[Stack overflow](https://stackoverflow.com/)*

- *[C# float.Parse String](https://stackoverflow.com/questions/27722032/c-sharp-float-parse-string)*
- *[Sorting a List](https://stackoverflow.com/questions/3738639/sorting-a-listint)*

#### *[.NET API](https://docs.microsoft.com/en-us/dotnet/api/?view=netcore-2.2)*

- *[ICollection Interface](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.icollection-1?view=netframework-4.8)*
- *[IComparer Interface](https://docs.microsoft.com/en-us/dotnet/api/system.collections.icomparer?view=netframework-4.8)*
- *[SortedList<TKey,TValue> Class](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.sortedlist-2?view=netframework-4.8)*
- *[IList Interface](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ilist-1?view=netframework-4.8)*
- *[Query (LINQ)](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/linq/)*
- *[List.Sort Method](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1.sort?view=netframework-4.8)*
- *[Action Delegate](https://docs.microsoft.com/en-us/dotnet/api/system.action-1?view=netframework-4.8)*
- *[Type.Equals Method](https://docs.microsoft.com/en-us/dotnet/api/system.type.equals?view=netframework-4.8)*
- *[Single.TryParse Method](https://docs.microsoft.com/en-us/dotnet/api/system.single.tryparse?view=netframework-4.8)*
- *[HashSet Class](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1?view=netframework-4.8)*
- *[FileStream Class](https://docs.microsoft.com/en-us/dotnet/api/system.io.filestream?view=netframework-4.8)*
- *[StreamReader Class](https://docs.microsoft.com/en-us/dotnet/api/system.io.streamreader?view=netframework-4.8)*
- *[GZipStream Class](https://docs.microsoft.com/en-us/dotnet/api/system.io.compression.gzipstream?view=netframework-4.8)*

**Nota:** Algumas das referências utilizadas não foram implementadas diretamente
mas a sua forma de funcionamento foi inspiração para criação de algoritmos
essenciais para o programa.
