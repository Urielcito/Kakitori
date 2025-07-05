# Kakitori
**An app that lets you extract japanese text with an OCR + A screenshot to make flashcards and study them with a custom SRS algorithm**

Kakitori combines functions from software as ShareX, with flashcards, specifically made for japanese learning.

It allows:
- Capturing japanese text in your screen with a custom hotkey.
- Spell the kanji out loud with TTS software (If you only want to know its reading)
- Save custom flashcards and progressively study them.

## Stack and Design Model

|`C#` | Backend |
| `WPF` | User Interface |
| `SQLite` | Database |
| `Tesseract OCR` | Japanese text recognition |
| `Azure TTS` | Text to Speech |
| `MVVM` | Design Model |

## Roadmap

- [x] Initial structure
- [] screenshot 
- [] Integration with Tesseract
- [] Flashcard creation and modification
- [] SQLite
- [] Custom SRS algorithm
- [] Flashcard study loop
- [] Export-Import of custom decks to Anki if desired

In active development, feedback and PR's are welcome.

## Gifs (nothing developed yet)

## Development process

I'm documenting the development process iteratively (with each commit)! Go check the `CHANGELOG.md`. Also check the Issues, it's where I'll document a simple to-do list.

## Inspiration and references

- [Anki](https://apps.ankiweb.net/)
- [Sharex](https://getsharex.com/)
- [Jisho.org](https://jisho.org/)
- [Tesseract OCR](https://github.com/tesseract-ocr/tesseract)