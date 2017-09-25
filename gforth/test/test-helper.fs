variable actual

: n$+! ( n addr -- )
    >r s>d <# #s #> r> $+! ;

: sp$+! ( addr -- )
    32 swap c$+! ;

: buffer-position ( x y o ? -- )
    s\" \n" actual $+!
    >r rot actual n$+!
    actual sp$+!
    swap actual n$+!
    actual sp$+!
    actual c$+!
    r> true = if
        s"  LOST" actual $+!
    endif ;

: run-test ( addr u -- addr1 u1 addr2 u2 )
    ['] buffer-position -rot read-instructions actual $@ ;

: str-test ( addr1 u1 addr2 u2 -- addr u )
    scent-reset
    actual $init
    run-test
    str= true ;

: err-test ( addr u -- )
    try run-test iferror 0 <> true then endtry ;
