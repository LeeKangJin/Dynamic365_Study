SELECT new_txt_request_type_detail2 AS 'Label'
      ,new_txt_request_type_detail2 AS 'Value'
FROM new_request_type
WHERE new_p_request_itme LIKE '%'+@request_item+'%'
AND   new_txt_reqeust_type_detail1 = @detail_request1
GROUP BY new_txt_request_type_detail2