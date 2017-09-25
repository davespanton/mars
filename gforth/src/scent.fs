variable scent 0 scent !

struct
    cell% field pos-prev
    cell% field pos-x
    cell% field pos-y
    char% field pos-o
end-struct pos%

: scent-len
    scent @ 0 = if 
        0
    else
        1 scent @ begin
        pos-prev @ dup 0 = invert while
            >r 1+ r> 
        repeat 
        drop
    endif ;

: scent= { x y o s-addr -- ? }
    s-addr @ 0 = if
        0
    else
        s-addr @ pos-x @ x = 
        s-addr @ pos-y @ y = and 
        s-addr @ pos-o c@ o = and 
    endif ;

: scent-contains { x y o s-addr -- ? }
    s-addr @ 0 = if
        0
    else
        x y o s-addr scent= invert if
            x y o s-addr @ pos-prev recurse
        else
            1
        endif
    endif ;

: scent-push ( x y o -- )
    pos% %alloc dup pos-prev scent @ swap ! scent !
    scent @ pos-o !
    scent @ pos-y !
    scent @ pos-x ! ;

: scent-reset ( -- )
    0 scent ! ;
