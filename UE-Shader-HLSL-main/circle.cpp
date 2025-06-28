float result = 0;

//Draw Circle
result = length(pos-uv) < size; 

//Draw multiple circles 
for (int i = 0 ; i < 100 ; i ++)
{
  float angle = (i / nSides) * (time) * 3.14;
  float2 pos = center + radius * float2(cos(angle), sin(angle));
  result += length(pos-uv) < size; //Draw Circle
}

return(result)
