#include <iostream>
using namespace std;

int main() {
    double a, b;
    int optiune;

    cout << "Introdu primul numar: ";
    cin >> a;

    cout << "Introdu al doilea numar: ";
    cin >> b;

    cout << "Alege operatia:\n";
    cout << "1. Adunare\n";
    cout << "2. Scadere\n";
    cout << "3. Inmultire\n";
    cout << "4. Impartire\n";

    cin >> optiune;

    if (optiune == 1) {
        cout << a + b;
    }
    else if (optiune == 2) {
        cout << a - b;
    }
    else if (optiune == 3) {
        cout << a * b;
    }
    else if (optiune == 4) {
        if (b != 0) {
            cout << a / b;
        } else {
            cout << "Eroare: impartire la 0";
        }
    }
    else {
        cout << "Optiune invalida";
    }

    return 0;
}
