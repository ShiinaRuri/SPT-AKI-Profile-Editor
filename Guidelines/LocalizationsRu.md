# SPT-AKI Profile Editor
[ENG README version](ENGREADME.md)

### Редактирование\создание файлов локализации:
[ENG version](/Guidlines/LocalizationsEng.md)

Файлы локализации генерируются в папке Localizations при первом запуске приложения.
Файлы можно редактировать, и создавать новые.
При запуске приложения они будут загружены, и отображены в соответствующем поле диалога настроек.

Файл локализации является JSON файлом со следующей структурой:
```json
{
  "Key": "en",
  "Name": "English",
  "Translations": {
    "key1": "Value1",
    "key2": "Value2",
    "key3": "Value3"
  }
}
```

Поле "Key": ключ локализации, должен соответствоать json файлу локализации из папки "dir_globals" сервера.
```json
\\ AppSettings.json
  "DirsList": {
    "dir_globals": "Aki_Data\\Server\\database\\locales\\global",
  }
```

Поле "Name": название локализации, для отображения в соответствующем поле диалога настроек.
Поле "Translations": набор строк "ключ": "значение". В поле "значение" вносятся переведенные фразы.

После редактирования\создания файла локализации рекомендуется проверить его валидность на [https://jsonformatter.curiousconcept.com/](https://jsonformatter.curiousconcept.com/) (или любой другой json валидатор)