

            https://stackblitz.com/angular/jxxlggybaxo?file=src%2Fapp%2Fexpansion-steps-example.html



 byte[] iv = new byte[16];
 byte[] array;

 using (Aes aes = Aes.Create())
 {
     aes.Key = Encoding.UTF8.GetBytes("01234567890123456789012345678901");
     aes.IV = iv;
     aes.Padding = PaddingMode.PKCS7;
     aes.Mode = CipherMode.CBC;

     ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

     using (MemoryStream memoryStream = new MemoryStream())
     {
         using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
         {
             using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
             {
                 streamWriter.Write("The quick brown fox jumps over the lazy dog");
             }

             array = memoryStream.ToArray();
         }
     }
 }

 string encryptstr = Convert.ToBase64String(array);






 byte[] iv = new byte[16];
 byte[] array;

 using (Aes aes = Aes.Create())
 {
     aes.Key = Encoding.UTF8.GetBytes("01234567890123456789012345678901");
     aes.IV = iv;
     aes.Padding = PaddingMode.PKCS7;
     aes.Mode = CipherMode.CBC;

     ICryptoTransform decryptor = aes.CreateDecryptor();

     using (MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(encrypt)))
     {
         using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
         {
             using (StreamReader streamReader = new StreamReader(cryptoStream))
             {
                 string decryptstr = streamReader.ReadToEnd();
             }

         }
     }
 }








  decryptData(key : any, ciphertextB64 : any) {                              // Base64 encoded ciphertext, 32 bytes string as key
    var Key = CryptoJS.enc.Utf8.parse(key);                             // Convert into WordArray (using Utf8)
    var iv = CryptoJS.lib.WordArray.create([0x00, 0x00, 0x00, 0x00]);   // Use zero vector as IV
    var decrypted = CryptoJS.AES.decrypt(ciphertextB64, Key, {iv: iv}); // By default: CBC, PKCS7 
    return decrypted.toString(CryptoJS.enc.Utf8);                       // Convert into string (using Utf8)
  }

  envcryptData(key : any, plaintext : any) {                              // Base64 encoded ciphertext, 32 bytes string as key
    var Key = CryptoJS.enc.Utf8.parse(key);                             // Convert into WordArray (using Utf8)
    var iv = CryptoJS.lib.WordArray.create([0x00, 0x00, 0x00, 0x00]);   // Use zero vector as IV
    var encrypted = CryptoJS.AES.encrypt(plaintext, Key, {iv: iv}); // By default: CBC, PKCS7 
    return encrypted.toString();                       // Convert into string (using Utf8)
  }



