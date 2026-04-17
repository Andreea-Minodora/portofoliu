#include <iostream>
#include <vector>
using namespace std;

int main()
{
    int n;
    cin >> n;

    vector<int> v(n);

    for(int i=0;i<n;i++){
        cin >> v[i];

    }

    cout<<"Alege optiunea:\n";
    cout<<"1. Suma \n";
    cout<<"2. Maxim \n";
    cout<<"3. Pare \n";
    cout<<"4. Pozitive \n";
    cout<<"5. Palindrom \n";
    cout<<"6. Egale \n";

    int optiune;
    cin >> optiune;
    if(optiune ==1){
        int s=0;
        for(int i=0;i<n;i++){
            s=s+v[i];
        }
    cout << s;
    }else if(optiune == 2){
        int maxim=v[0];
        for(int i=0;i<n;i++){
            if(v[i]>maxim){
                maxim=v[i];
            }
        }
        cout<< maxim;
    }else if(optiune == 3){
        int cnt=0;
        for(int i=0;i<n;i++){
            if(v[i] % 2 == 0){
                cnt ++;
            }
        }
        cout << cnt;

        }else if(optiune == 4){
        int nrpoz=0;
        for(int i=0;i<n;i++){
            if(v[i] > 0){
                nrpoz++;

            }
        }
        cout << nrpoz;
        }else if(optiune ==5){

        bool palindrom =true;
        for(int i=0;i<n/2;i++){
            if(v[i] != v[n-i-1]){
                palindrom=false;
            }
        }
        if(palindrom){
            cout<<"Este palindrom";
        }else {
            cout << "Nu este palindrom";
        }
        }else if(optiune==6){
            bool egal=true;
        for(int i=  1;i<n;i++){
            if(v[i] != v[0]){
                    egal=false;
            }
        }
        if(egal){
            cout<<"sunt egale";

        }else{
            cout<< "nu sunt egale";
        }
        }


        }



