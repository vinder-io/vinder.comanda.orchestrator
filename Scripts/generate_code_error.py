## script to generate consistent error codes based on input string
## it calculates MD5 hash, takes first 5 characters, and appends to the prefix

import hashlib

def generate_error_code():
    entry = input("Enter the error name (e.g., #PROJECT-ERROR-PRODUCT-ALREADY-EXISTS): ").strip()

    if not entry:
        print("Empty input! Try again.")
        return

    hash_md5 = hashlib.md5(entry.encode("utf-8")).hexdigest().upper()
    short_hash = hash_md5[:5]

    parts = entry.split("-")
    if len(parts) < 3:
        print("Invalid format. Use something like #PROJECT-ERROR-THING")
        return

    prefix = "-".join(parts[:3])
    final_code = f"{prefix}-{short_hash}"

    print(f"Generated error code: {final_code}")

if __name__ == "__main__":
    generate_error_code()
