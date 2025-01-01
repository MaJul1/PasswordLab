# PasswordLab

PasswordLab is a command-line tool for encrypting and decrypting files using AES encryption. It provides a simple interface to secure your files with a password.

## Table of Contents

- [PasswordLab](#passwordlab)
  - [Table of Contents](#table-of-contents)
  - [Installation](#installation)
    - [Prerequisites](#prerequisites)
    - [Installing](#installing)
  - [Usage](#usage)
    - [CLI Commands](#cli-commands)
    - [Encryption](#encryption)
      - [Encrypt a File](#encrypt-a-file)
      - [Encrypt a File to a Designated Output Path](#encrypt-a-file-to-a-designated-output-path)
      - [Encrypt a Folder](#encrypt-a-folder)
      - [Encrypt Multiple Components](#encrypt-multiple-components)
    - [Decryption](#decryption)
      - [Decrypt a Encrypted File](#decrypt-a-encrypted-file)
  - [License](#license)

## Installation

### Prerequisites

- .NET SDK 8.0 or later

### Installing

1. Install via nuget.org
```sh 
dotnet tool install --global PasswordLab --version "1.1.0"
```

## Usage

### CLI Commands

| Command       | Description                                                                 | Example                                                                                     |
|---------------|-----------------------------------------------------------------------------|---------------------------------------------------------------------------------------------|
| `encrypt`     | Encrypt a file, folder, or multiple components                              | `passwordlab encrypt "file-path" -o "output-path (optional)" -n "encrypted-file-name" --password "password"` |
| `decrypt`     | Decrypt a file, folder, or multiple components                              | `passwordlab decrypt "file-path" -o "output-path (optional)" --password "password"`       |
| `help`        | Display help information for the commands                                   | `passwordlab --help`                                                                        |
| `version`     | Display version information                                                 | `passwordlab --version`                                                                     |

### Encryption

#### Encrypt a File
To encrypt a file, use the command `encrypt` with the file path. Use the `-n, --name` to specify the output file name, and `--password` to specify the password.

```sh
passwordlab encrypt "C://file-path-to-encrypt" -n "encrypted-file-name" --password "password"
```
This will save a `.gz` file that is ready for decryption.

#### Encrypt a File to a Designated Output Path
To encrypt a file to a designated output, use the `-o, --output` option to specify the output file path. The current active directory is used as the default if not specified.

```sh
passwordlab encrypt "C://file-path-to-encrypt" -n "encrypted-file-name" -o "C://output-file-path" --password "password"
```

#### Encrypt a Folder
To encrypt a folder, just copy the path of the directory.

```sh
passwordlab encrypt "C://folder-path-to-encrypt" -n "encrypted-file-name" --password "password"
```
Remember that empty folder will not be included in encryption.

#### Encrypt Multiple Components
To encrypt multiple files and folders, specify each path using the `-f` option.

```sh
passwordlab encrypt "C://folder-path-1" "C://file-path-2" "C://folder-path-3" -n "encrypted-file-name" --password "password"
```

### Decryption

#### Decrypt a Encrypted File
To decrypt a file, use the command `decrypt` with the file path including the options `-o` to specify the output path, and `--password` to specify the password. `-o` is optional and still use the current active directory for extraction of encrypted files.

```sh
passwordlab decrypt -f "C://file-path-to-decrypt" -o "C://output-file-path" --password "password"
```

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.